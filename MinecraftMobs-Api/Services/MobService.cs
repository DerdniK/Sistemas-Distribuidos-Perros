using System.ServiceModel;
using MinecraftMobs_Api.Contracts;
using MinecraftMobs_Api.Dtos;
using MinecraftMobs_Api.Mappers;
using MinecraftMobs_Api.Models;
using MinecraftMobs_Api.Repositories;
using MinecraftMobs_Api.Validators;

namespace MinecraftMobs_Api.Services;

public class MobService : IMobService
{
    public readonly IMobRepository _mobRepository;
    public MobService(IMobRepository mobRepository)
    {
        _mobRepository = mobRepository;
    }


    public async Task<MobReponseDeleteDto> DeleteMob(Guid Id, CancellationToken cancellationToken)
    {
        var mob = await _mobRepository.GetMobByIdAsync(Id, cancellationToken);
        if (!MobExists(mob))
        {
            throw new FaultException("Mob not found");
        }

        await _mobRepository.DeleteMobAsync(mob, cancellationToken);
        return new MobReponseDeleteDto { Succes = true };
    }

    public async Task<MobResponseDto> GetMobById(Guid Id, CancellationToken cancellationToken)
    {
        var mob = await _mobRepository.GetMobByIdAsync(Id, cancellationToken);
        return MobExists(mob) ? mob.ToResponseDto() : throw new FaultException("Mob id not found");
    }

    public async Task<MobResponseDto> GetMobByName(string name, CancellationToken cancellationToken)
    {
        var mob = await _mobRepository.GetMobByNameAsync(name, cancellationToken);
        return MobExists(mob) ? mob.ToResponseDto() : throw new FaultException("Mob name not found");
    }

    public async Task<MobResponseDto> CreateMob(CreateMobDto mobRequest, CancellationToken cancellationToken)

    {
        mobRequest.ValidateArmor().ValidateAttack().ValidateBehaviour().ValidateHealth().ValidateName().ValidateSpeed().ValidateVariant();
        if (await MobAlreadyExists(mobRequest.Name, cancellationToken))
        {
            throw new FaultException("Mob already exists");
        }

        var mob = await _mobRepository.CreateMobAsync(mobRequest.ToModel(), cancellationToken);

        return mob.ToResponseDto();
    }

    private async Task<bool> MobAlreadyExists(string name, CancellationToken cancellationToken)
    {
        var mobs = await _mobRepository.GetMobByNameAsync(name, cancellationToken);
        return MobExists(mobs);
    }
    
    private static bool MobExists(Mob mob) {
        return mob is not null;
    }
}