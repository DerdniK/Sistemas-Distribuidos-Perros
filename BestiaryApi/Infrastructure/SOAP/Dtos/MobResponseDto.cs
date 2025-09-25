using System.Runtime.Serialization;

namespace BestiaryApi.Infrastructure.SOAP.Dtos;

[DataContract(Name = "MobResponseDto", Namespace = "http://minecraftMob-api/minecraftMob-service")]
public class MobResponseDto
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
    public required StatsDto Stats{ get; set; }
}