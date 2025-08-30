namespace MinecraftMobs_Api.Infrastructure.Entities;

public class MobEntity
{
    public Guid Id { get; set; } // Example: {050d9528-e69d-4d91-8096-a34eb526c91a}
    public string? Name { get; set; } // Example: Zombie
    public string? Variant { get; set; } // Example: Hush
    public string? Behaviour { get; set; } // Example: Passive/Neutral/Aggressive
    //? MobStats
    public int Health { get; set; } // Example: 20
    public int Attack { get; set; } // Example: 5
    public int Armor { get; set; } // Example: 2
    public int Speed{ get; set; } // Example: 10
}