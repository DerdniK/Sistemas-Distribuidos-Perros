using System.Runtime.Serialization;
namespace MinecraftMobs_Api.Dtos;

[DataContract(Name = "StatsDto", Namespace = "http://minecraftMob-api/minecraftMob-service")]
public class StatsDto
{
    [DataMember(Name = "Health", Order = 1)]
    public int Health { get; set; } // Example: 20

    [DataMember(Name = "Attack", Order = 2)]
    public int Attack { get; set; } // Example: 5

    [DataMember(Name = "Armor", Order = 3)]
    public int Armor { get; set; } // Example: 2

    [DataMember(Name = "Speed", Order = 4)]
    public int Speed { get; set; } // Example: 10
}