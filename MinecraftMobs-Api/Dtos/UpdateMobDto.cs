using System.Runtime.Serialization;
namespace MinecraftMobs_Api.Dtos;

[DataContract(Name = "UpdateMobDto", Namespace = "http://minecraftMob-api/minecraftMob-service")]
public class UpdateMobDto
{
    [DataMember(Name = "Id", Order = 1)]
    public Guid Id { get; set; }

    [DataMember(Name = "Name", Order = 2)]
    public string Name { get; set; }

    [DataMember(Name = "Variant", Order = 3)]
    public string Variant { get; set; }
    [DataMember(Name = "Behaviour", Order = 4)]
    public string Behaviour { get; set; }

    [DataMember(Name = "Stats", Order = 5)]
    public StatsDto Stats { get; set; }
}