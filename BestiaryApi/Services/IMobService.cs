using BestiaryApi.Dtos;
using BestiaryApi.Models;

namespace BestiaryApi.Services;

public interface IMobService
{
    Task<Mob> GetMobByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Mob> CreateMobAsync(Mob mob, CancellationToken cancellationToken);
    // Task<IList<Mob>> GetMobsAsync(string name, string type, CancellationToken cancellationToken);
    Task<IList<Mob>> GetMobsByNameAsync(string name, CancellationToken cancellationToken);
    Task DeleteMobAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateMobAsync(Mob mob, CancellationToken cancellationToken);
    Task<Mob> PatchMobAsync(Guid id, PatchMobRequest mobRequest, CancellationToken cancellationToken);
    Task<IList<Mob>> GetAllMobsAsync(CancellationToken cancellationToken);


}