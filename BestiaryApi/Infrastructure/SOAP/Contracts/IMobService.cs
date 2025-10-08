using System.ServiceModel;
using BestiaryApi.Infrastructure.SOAP.Dtos;
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
    Task<IList<MobResponseDto>> GetMobsByBehaviour(string name, CancellationToken cancellationToken);

    [OperationContract]
    Task<MobReponseDeleteDto> DeleteMob(Guid Id, CancellationToken cancellationToken);
    
    [OperationContract]
    Task<MobResponseDto> UpdateMob(UpdateMobDto pokemon, CancellationToken cancellationToken);
}