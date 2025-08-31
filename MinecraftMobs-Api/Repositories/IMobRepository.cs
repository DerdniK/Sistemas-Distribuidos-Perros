using MinecraftMobs_Api.Models;

namespace MinecraftMobs_Api.Repositories;

public interface IMobRepository
{
    Task<Mob> GetMobByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<Mob> CreateMobAsync(Mob mob, CancellationToken cancellationToken);
    Task<Mob> GetMobByNameAsync(string name, CancellationToken cancellationToken);
}