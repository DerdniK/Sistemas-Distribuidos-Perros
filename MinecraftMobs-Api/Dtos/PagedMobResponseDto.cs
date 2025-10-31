using System.Runtime.Serialization;

namespace MinecraftMobs_Api.Dtos
{
    [DataContract(Name = "PagedMobResponseDto", Namespace = "http://minecraftMob-api/minecraftMob-service")]
    public class PagedMobResponseDto
    {
        [DataMember(Order = 1)]
        public int PageNumber { get; set; }

        [DataMember(Order = 2)]
        public int PageSize { get; set; }

        [DataMember(Order = 3)]
        public int TotalRecords { get; set; }

        [DataMember(Order = 4)]
        public int TotalPages { get; set; }

        [DataMember(Order = 5)]
        public IList<MobResponseDto> Data { get; set; } = new List<MobResponseDto>();
    }
}
