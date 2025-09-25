using BestiaryApi.Models;

namespace BestiaryApi.Services;

public interface IMobService
{
    Task<Mob> GetMobByIdAsync(Guid id, CancellationToken cancellationToken);
}