using BestiaryApi.Models;

namespace BestiaryApi.Gateways;

public interface IMobGateway
{
    Task<Mob> GetMobByIdAsync(Guid id, CancellationToken cancellationToken);
}