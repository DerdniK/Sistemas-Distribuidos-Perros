namespace MinecraftMobs_Api.Models;

public class Mob
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Variant { get; set; }
    public string? Behaviour { get; set; }
    public Stats Stats { get; set; }
}