using System.Runtime.Serialization;

namespace MinecraftMobs_Api.Dtos;

[DataContract(Name = "MobListResponseDto", Namespace = "http://minecraftMob-api/minecraftMob-service")]
public class MobListResponseDto
{
    [DataMember(Order = 1)]
    public required IList<MobResponseDto> Mobs { get; set; }

    [DataMember(Order = 2)]
    public int PageNumber { get; set; }

    [DataMember(Order = 3)]
    public int PageSize { get; set; }

    [DataMember(Order = 4)]
    public int TotalCount { get; set; }

    [DataMember(Order = 5)]
    public int TotalPages { get; set; }
}
