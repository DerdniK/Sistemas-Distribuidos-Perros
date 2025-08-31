
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

    public async Task<Mob> GetMobByNameAsync(string name, CancellationToken cancellationToken)
    {
        var mob = await _context.Mobs.AsNoTracking().FirstOrDefaultAsync(s => s.Name.Contains(name));
        return mob.ToModel();
    }
}