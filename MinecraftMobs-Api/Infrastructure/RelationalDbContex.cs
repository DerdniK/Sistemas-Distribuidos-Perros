using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using MinecraftMobs_Api.Infrastructure.Entities;

namespace MinecraftMobs_Api.Infrastructure;

public class RelationalDbContext : DbContext
{
    public DbSet<MobEntity> Mobs { get; set; }
    public RelationalDbContext(DbContextOptions<RelationalDbContext> db) : base(db) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MobEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Variant).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Behaviour).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Health).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Attack).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Armor).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Speed).IsRequired().HasMaxLength(10);
        });
    }
}