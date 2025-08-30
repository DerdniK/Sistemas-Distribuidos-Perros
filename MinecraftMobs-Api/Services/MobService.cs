using System.ServiceModel;
using MinecraftMobs_Api.Contracts;
using MinecraftMobs_Api.Dtos;

namespace MinecraftMobs_Api.Services;

public class MobService : IMobService
{
    public Task<MobResponseDto> CreateMob(CreateMobDto mob, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}