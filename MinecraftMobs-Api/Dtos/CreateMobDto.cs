using System.Runtime.Serialization;

namespace MinecraftMobs_Api.Dtos;

[DataContract(Name = "CreateMobDto", Namespace = "http://minecraftMob-api/minecraftMob-service")]
public class CreateMobDto
{
    [DataMember(Name = "Name", Order = 1)]
    public string? Name { get; set; } 

    [DataMember(Name = "Variant", Order = 2)]
    public string? Variant { get; set; } 

    [DataMember(Name = "Behaviour", Order = 3)]
    public string? Behaviour { get; set; } 

    [DataMember(Name = "Stats", Order = 4)] 
    public required StatsDto Stats { get; set; } // Like json {Health: 10, Speed: 10, ...}
}