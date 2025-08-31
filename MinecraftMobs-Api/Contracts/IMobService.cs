using System.ServiceModel;
using MinecraftMobs_Api.Dtos;

namespace MinecraftMobs_Api.Contracts;

[ServiceContract(Name = "MobService", Namespace = "http://minecraftMob-api/minecraftMob-service")]
public interface IMobService
{
    [OperationContract]
    Task<MobResponseDto> CreateMob(CreateMobDto mob, CancellationToken cancellationToken);

    [OperationContract]
    Task<MobResponseDto> GetMobById(Guid Id, CancellationToken cancellationToken);

    [OperationContract]
    Task<MobResponseDto> GetMobByName(string name, CancellationToken cancellationToken);
}