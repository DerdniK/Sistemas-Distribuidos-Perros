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
    
    public async IAsyncEnumerable<Trainer> GetTrainersByName(string name, CancellationToken cancellationToken)
    {
        var request = new TrainersByNameRequest { Name = name };
        using var call = _client.GetTrainersByName(request, cancellationToken: cancellationToken);
        while (await call.ResponseStream.MoveNext(cancellationToken))
        {
            yield return ToModel(call.ResponseStream.Current);
        }
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