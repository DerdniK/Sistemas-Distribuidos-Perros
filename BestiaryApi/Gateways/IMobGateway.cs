using BestiaryApi.Models;

namespace BestiaryApi.Gateways;

public interface IMobGateway
{
    Task<Mob> GetMobByIdAsync(Guid id, CancellationToken cancellationToken);
    // Task<IList<Mob>> GetMobsByBehaviorAsync(string name, CancellationToken cancellationToken);
    Task<IList<Mob>> GetMobsByBehaviourAsync(string name, CancellationToken cancellationToken);
    Task<Mob> CreateMobAsync(Mob mob, CancellationToken cancellationToken);
    Task DeleteMobAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateMobAsync(Mob mob, CancellationToken cancellationToken);
    Task<IList<Mob>> GetAllMobsAsync(CancellationToken cancellationToken);

}