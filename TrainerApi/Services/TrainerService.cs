using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TrainerApi.Mappers;
using TrainerApi.Models;
using TrainerApi.Repositories;

namespace TrainerApi.Services;

public class TrainerService : TrainerApi.TrainerService.TrainerServiceBase
{
    private readonly ITrainerRepository _trainerRepository;
    private static int LegalMexicanAge = 18;

    public TrainerService(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }
    public override async Task<TrainerResponse> GetTrainerById(TrainerByIdRequest request, ServerCallContext context)
    {

        var trainer = await GetTrainerAsync(request.Id, context.CancellationToken);
        return trainer.ToResponse();
    }

    public override async Task<Empty> UpdateTrainer(UpdateTrainerRequest request, ServerCallContext context)
    {
        if (!IdFormatIsValid(request.Id))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid trainer Id"));
        if (request.Age < LegalMexicanAge)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid trainer Age"));
        if (string.IsNullOrEmpty(request.Name) || request.Name.Length < 3)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Your name is invalid"));

        var trainer = await GetTrainerAsync(request.Id, context.CancellationToken);
        trainer.Name = request.Name;
        trainer.Age = request.Age;
        trainer.Birthdate = request.Birthdate.ToDateTime();
        trainer.Medals = request.Medals.Select(s => s.ToModel()).ToList();
        var trainers = await _trainerRepository.GetByNameAsync(request.Name, context.CancellationToken);
        var trainerAlreadyExists = trainers.Any(t => t.Id != trainer.Id);
        if (trainerAlreadyExists)
            throw new RpcException(new Status(StatusCode.AlreadyExists, "Trainer already exists"));

        await _trainerRepository.UpdateAsync(trainer, context.CancellationToken);

        return new Empty();
    }

    public override async Task<Empty> DeleteTrainer(TrainerByIdRequest request, ServerCallContext context)
    {
        if (!IdFormatIsValid(request.Id))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid trainer Id"));

        var trainer = await GetTrainerAsync(request.Id, context.CancellationToken);
        await _trainerRepository.DeleteAsync(trainer.Id, context.CancellationToken);
        return new Empty();
    }

    public override async Task<CreateTrainerResponse> CreateTrainers(IAsyncStreamReader<CreateTrainerRequest> requestStream, ServerCallContext context)
    {
        var createdTrainers = new List<TrainerResponse>();

        while (await requestStream.MoveNext(context.CancellationToken))
        {
            var request = requestStream.Current;
            var trainer = request.ToModel();
            var trainerExists = await _trainerRepository.GetByNameAsync(trainer.Name, context.CancellationToken);

            if (trainerExists.Any())
                continue;
            var createdTrainer = await _trainerRepository.CreateAsync(trainer, context.CancellationToken);
            createdTrainers.Add(createdTrainer.ToResponse());
        }

        return new CreateTrainerResponse
        {
            SuccessCount = createdTrainers.Count,
            Trainers = { createdTrainers }
        };
    }

    public override async Task GetTrainersByName(TrainersByNameRequest request, IServerStreamWriter<TrainerResponse> responseStream, ServerCallContext context)
    {
        var trainers = await _trainerRepository.GetByNameAsync(request.Name, context.CancellationToken);

        foreach (var trainer in trainers)
        {
            if (context.CancellationToken.IsCancellationRequested)
                break;
            await responseStream.WriteAsync(trainer.ToResponse());
            await Task.Delay(TimeSpan.FromSeconds(5), context.CancellationToken);
        }
    }

    private async Task<Trainer> GetTrainerAsync(string id, CancellationToken cancellationToken)
    {
        var trainer = await _trainerRepository.GetTrainerByIdAsync(id, cancellationToken);
        return trainer ?? throw new RpcException(new Status(StatusCode.NotFound, $"Trainer with ID {id} not found."));
    }
    
    private static bool IdFormatIsValid(string id)
    {
        return !string.IsNullOrWhiteSpace(id) && id.Length > 20;
    }
}