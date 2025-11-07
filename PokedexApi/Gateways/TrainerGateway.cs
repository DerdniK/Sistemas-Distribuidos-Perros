using Google.Protobuf.Collections;
using Grpc.Core;
using PokedexApi.Exceptions;
using PokedexApi.Infrastructure.Grpc;
using PokedexApi.Models;

namespace PokedexApi.Gateways;

public class TrainerGateway : ITrainerGateway
{
    private readonly TrainerService.TrainerServiceClient _client;

    public TrainerGateway(TrainerService.TrainerServiceClient client)
    {
        _client = client;
    }

    public async Task UpdateTrainerAsync(Trainer trainer, CancellationToken cancellationToken)
    {
        try
        {
            var request = new UpdateTrainerRequest
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Age = trainer.Age,
                Birthdate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(trainer.Birthdate.ToUniversalTime()),
                Medals = { trainer.Medals.Select(m => new Infrastructure.Grpc.Medal
                {
                    Region = m.Region,
                    Type = (Infrastructure.Grpc.MedalType)m.Type
                }) }
            };

            await _client.UpdateTrainerAsync(request, cancellationToken: cancellationToken);
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            throw new TrainerNotFoundException(trainer.Id);
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.AlreadyExists)
        {
            throw new TrainerAlreadyExistsException(trainer.Name);
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.InvalidArgument)
        {
            throw new TrainerValidationException(ex.Status.Detail);
        }
    }

    public async Task DeleteTrainerAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            await _client.DeleteTrainerAsync(new TrainerByIdRequest { Id = id },
             cancellationToken: cancellationToken);
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            throw new TrainerNotFoundException(id);
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.InvalidArgument)
        {
            throw new TrainerInvalidIdException(id);
        }
    }

    public async Task<Trainer> GetTrainerById(string id, CancellationToken cancellationToken)
    {
        try
        {
            var trainer = await _client.GetTrainerByIdAsync(new TrainerByIdRequest { Id = id }, cancellationToken: cancellationToken);
            return ToModel(trainer);
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            throw new TrainerNotFoundException(id);
        }
    }

    public async Task<(int, IList<Trainer>)> CreateTrainersAsync(IEnumerable<Trainer> trainers, CancellationToken cancellationToken)
    {
        using var call = _client.CreateTrainers(cancellationToken: cancellationToken);
        foreach (var trainer in trainers)
        {
            var request = new CreateTrainerRequest
            {
                Name = trainer.Name,
                Age = trainer.Age,
                Birthdate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(trainer.Birthdate),
                Medals = { trainer.Medals.Select(m => new Infrastructure.Grpc.Medal
                {
                    Region = m.Region,
                    Type = (Infrastructure.Grpc.MedalType) m.Type
                }) }

            };
            await call.RequestStream.WriteAsync(request, cancellationToken);
            await Task.Delay(1000, cancellationToken); // 1 sec
        }
        await call.RequestStream.CompleteAsync();
        var response = await call;
        return (response.SuccessCount,  ToModel(response.Trainers));
    }

    public async IAsyncEnumerable<Trainer> GetTrainersByName(string name, CancellationToken cancellationToken)
    {
        var request = new TrainersByNameRequest { Name = name };
        using var call = _client.GetTrainersByName(request, cancellationToken: cancellationToken);
        while (await call.ResponseStream.MoveNext(cancellationToken))
        {
            yield return ToModel(call.ResponseStream.Current);
        }
    }
    
    private static IList<Trainer> ToModel(RepeatedField<TrainerResponse> trainerResponses)
    {
        return trainerResponses.Select(ToModel).ToList();
    }


    private static Trainer ToModel(TrainerResponse trainerResponse)
    {
        return new Trainer
        {
            Id = trainerResponse.Id,
            Name = trainerResponse.Name,
            Age = trainerResponse.Age,
            Birthdate = trainerResponse.Birthdate.ToDateTime(),
            CreatedAt = trainerResponse.CreatedAt.ToDateTime(),
            Medals = trainerResponse.Medals.Select(ToModel).ToList()
        };
    }

    private static Models.Medal ToModel(Infrastructure.Grpc.Medal medal)
    {
        return new Models.Medal
        {
            Region = medal.Region,
            Type = (Models.MedalType)medal.Type
        };
    }
}