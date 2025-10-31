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


    public async Task<PagedMobResponseDto> GetMobsPaginated(string? name, int pageNumber, int pageSize, string? orderBy, string? orderDirection, CancellationToken cancellationToken)
    {
        var mobs = await _mobRepository.GetAllAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(name))
        {
            mobs = mobs.Where(m => m.Behaviour?.Contains(name, StringComparison.OrdinalIgnoreCase) == true).ToList();
        }

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            bool ascending = string.IsNullOrWhiteSpace(orderDirection) || orderDirection.ToLower() == "asc";

            mobs = orderBy.ToLower() switch
            {
                "name" => ascending ? mobs.OrderBy(m => m.Name).ToList() : mobs.OrderByDescending(m => m.Name).ToList(),
                "type" => ascending ? mobs.OrderBy(m => m.Variant).ToList() : mobs.OrderByDescending(m => m.Variant).ToList(),
                "behaviour" => ascending ? mobs.OrderBy(m => m.Behaviour).ToList() : mobs.OrderByDescending(m => m.Behaviour).ToList(),
                _ => mobs
            };
        }

        int totalRecords = mobs.Count;

        var pagedMobs = mobs.Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        var data = pagedMobs.ToResponseDto();

        return new PagedMobResponseDto
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            Data = data
        };
    }




    public async Task<MobResponseDto> UpdateMob(UpdateMobDto mobToUpdate, CancellationToken cancellationToken)
    {
        var mob = await _mobRepository.GetMobByIdAsync(mobToUpdate.Id, cancellationToken);
        if (!MobExists(mob))
        {
            throw new FaultException("Mob not found");
        }

        // var duplicatedPokemon = await _pokemonRepository.GetByNameAsync(pokemonToUpdate.Name, cancellationToken);
        // duplicatedPokemon != null && duplicatedPokemon.Id != pokemonToUpdate.Id
        if (!await IsMobAllowedToBeUpdated(mobToUpdate, cancellationToken))
        {
            throw new FaultException("Mob with the same name already exists");
        }

        mob.Name = mobToUpdate.Name;
        mob.Variant = mobToUpdate.Variant;
        mob.Behaviour = mobToUpdate.Behaviour;
        mob.Stats.Health = mobToUpdate.Stats.Health;
        mob.Stats.Attack = mobToUpdate.Stats.Attack;
        mob.Stats.Armor = mobToUpdate.Stats.Armor;
        mob.Stats.Speed = mobToUpdate.Stats.Speed;

        await _mobRepository.UpdateMobAsync(mob, cancellationToken);
        return mob.ToResponseDto();
    }

    private async Task<bool> IsMobAllowedToBeUpdated(UpdateMobDto mobToUpdate, CancellationToken cancellationToken)
    {
        var duplicatedMob = await _mobRepository.GetByNameAsync(mobToUpdate.Name, cancellationToken);
        //pokemon duplicado != no existe y es el mismo pokemon son falsos
        return duplicatedMob == null || IsTheSameMob(duplicatedMob, mobToUpdate);

    }

    private static bool IsTheSameMob(Mob mob, UpdateMobDto MobToUpdate)
    {
        // charizard != pikachu = true
        return mob.Id == MobToUpdate.Id;
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

    public async Task<IList<MobResponseDto>> GetMobsByBehaviour(string name, CancellationToken cancellationToken)
    {
        var mobs = await _mobRepository.GetMobsByBehaviourAsync(name, cancellationToken);
        return mobs.ToResponseDto();
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
        var mobs = await _mobRepository.GetByNameAsync(name, cancellationToken);
        return MobExists(mobs);
    }
    
    private static bool MobExists(Mob mob) {
        return mob is not null;
    }
}