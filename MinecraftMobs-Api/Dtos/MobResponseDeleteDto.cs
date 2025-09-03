using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MinecraftMobs_Api.Dtos;

[DataContract(Name = "MobReponseDeleteDto", Namespace = "http://minecraftMob-api/minecraftMob-service")]
public class MobReponseDeleteDto
{
    [DataMember(Name = "Succes", Order = 1)]
    public bool Succes { get; set; }
}