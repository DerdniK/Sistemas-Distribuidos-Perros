using System.Reflection.Emit;
using BestiaryApi.Dtos;
using BestiaryApi.Expections;
using BestiaryApi.Gateways;
using BestiaryApi.Models;

namespace BestiaryApi.Services;

public class MobService : IMobService
{
    private readonly IMobGateway _mobGateway;

    public MobService(IMobGateway mobGateway)
    {
        _mobGateway = mobGateway;
    }

    public async Task<IList<Mob>> GetAllMobsAsync(CancellationToken cancellationToken)
    {
        return await _mobGateway.GetAllMobsAsync(cancellationToken);
    }


    public async Task<Mob> PatchMobAsync(Guid id, PatchMobRequest mobRequest, CancellationToken cancellationToken)
    {
        var mob = await _mobGateway.GetMobByIdAsync(id, cancellationToken);
        if (mob is null)
        {
            throw new MobNotFoundException(id);
        }

        mob.Name = mobRequest.Name ?? mob.Name;
        mob.Variant = mobRequest.Variant ?? mob.Variant;
        mob.Behaviour = mobRequest.Behaviour ?? mob.Behaviour;

        if (mobRequest.Stats is not null)
        {
            mob.Stats.Health = mobRequest.Stats.Health ?? mob.Stats.Health;
            mob.Stats.Attack = mobRequest.Stats.Attack ?? mob.Stats.Attack;
            mob.Stats.Armor = mobRequest.Stats.Armor ?? mob.Stats.Armor;
            mob.Stats.Speed = mobRequest.Stats.Speed ?? mob.Stats.Speed;
        }

        await _mobGateway.UpdateMobAsync(mob, cancellationToken);
        return mob;
    }

    public async Task UpdateMobAsync(Mob mob, CancellationToken cancellationToken)
    {
        await _mobGateway.UpdateMobAsync(mob, cancellationToken);
    }

    public async Task DeleteMobAsync(Guid id, CancellationToken cancellationToken)
    {
        await _mobGateway.DeleteMobAsync(id, cancellationToken);
    }

    public async Task<Mob> CreateMobAsync(Mob mob, CancellationToken cancellationToken)
    {
        var mobs = await _mobGateway.GetMobsByBehaviourAsync(mob.Name, cancellationToken);
        if (MobExists(mobs, mob.Name))
        {
            throw new MobAlreadyExistsException(mob.Name);
        }

        return await _mobGateway.CreateMobAsync(mob, cancellationToken);
    }

    public async Task<IList<Mob>> GetMobsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _mobGateway.GetMobsByBehaviourAsync(name, cancellationToken);
    }

    public async Task<Mob> GetMobByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _mobGateway.GetMobByIdAsync(id, cancellationToken);
    }

    private static bool MobExists(IList<Mob> mobs, string mobNameToSearch)
    {
        return mobs.Any(s => s.Name.ToLower().Equals(mobNameToSearch.ToLower()));
    }

    // public async Task<IList<Mob>> GetMobsAsync(string name, string variant, CancellationToken cancellationToken)
    // {
    //     var mobs = await _mobGateway.GetMobsByNameAsync(name, cancellationToken);
    //     return mobs.Where(s => s.Variant.ToLower().Contains(variant.ToLower())).ToList();
    // }
}