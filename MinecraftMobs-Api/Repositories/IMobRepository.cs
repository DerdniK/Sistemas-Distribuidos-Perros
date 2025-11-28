using MinecraftMobs_Api.Dtos;
using MinecraftMobs_Api.Models;

namespace MinecraftMobs_Api.Repositories;

public interface IMobRepository
{
    Task<Mob> GetMobByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<Mob> CreateMobAsync(Mob mob, CancellationToken cancellationToken);
    Task<IReadOnlyList<Mob>> GetMobsByBehaviourAsync(string name, CancellationToken cancellationToken);
    Task DeleteMobAsync(Mob mob, CancellationToken cancellationToken);
    Task UpdateMobAsync(Mob mob, CancellationToken cancellationToken);
    Task<Mob> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<IList<Mob>> GetAllAsync(CancellationToken cancellationToken);


}