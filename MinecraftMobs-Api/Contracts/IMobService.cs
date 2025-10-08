using System.ServiceModel;
using MinecraftMobs_Api.Dtos;
using MinecraftMobs_Api.Models;

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
    
    [OperationContract]
    Task<PagedMobResponseDto> GetMobsPaginated(string? name, int pageNumber, int pageSize, string? orderBy, string? orderDirection, CancellationToken cancellationToken);


}