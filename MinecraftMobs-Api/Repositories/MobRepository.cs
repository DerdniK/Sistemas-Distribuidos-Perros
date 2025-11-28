
using Microsoft.EntityFrameworkCore;
using MinecraftMobs_Api.Infrastructure;
using MinecraftMobs_Api.Mappers;
using MinecraftMobs_Api.Models;

namespace MinecraftMobs_Api.Repositories;

public class MobRepository : IMobRepository
{
    private readonly RelationalDbContext _context;
    public MobRepository(RelationalDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Mob>> GetAllAsync(CancellationToken cancellationToken)
    {
        var mobs = await _context.Mobs.AsNoTracking().ToListAsync(cancellationToken);
        return mobs.ToModel();
    }

    public async Task<Mob> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var mob = await _context.Mobs.AsNoTracking().FirstOrDefaultAsync(s => s.Name.Contains(name));
        return mob.ToModel();
    }

     public async Task UpdateMobAsync(Mob mob, CancellationToken cancellationToken)
    {
        _context.Mobs.Update(mob.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteMobAsync(Mob mob, CancellationToken cancellationToken)
    {
        _context.Mobs.Remove(mob.ToEntity());
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Mob> CreateMobAsync(Mob mob, CancellationToken cancellationToken)
    {
        var mobToCreate = mob.ToEntity();
        mobToCreate.Id = Guid.NewGuid();

        await _context.Mobs.AddAsync(mobToCreate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return mobToCreate.ToModel();
    }

    public async Task<Mob> GetMobByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        var mob = await _context.Mobs.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id, cancellationToken);
        return mob.ToModel();
    }

    public async Task<IReadOnlyList<Mob>> GetMobsByBehaviourAsync(string behaviour, CancellationToken cancellationToken)
    {
         var mobs = await _context.Mobs.AsNoTracking().Where(s => s.Behaviour.Contains(behaviour)).ToListAsync(cancellationToken);

        return mobs.ToReadOnlyModel();
    }
}