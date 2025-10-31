using BestiaryApi.Dtos;
using BestiaryApi.Infrastructure.SOAP.Dtos;
using BestiaryApi.Models;


using RestMobResponse = BestiaryApi.Dtos.MobResponse;
using SoapMobResponseDto = BestiaryApi.Infrastructure.SOAP.Dtos.MobResponseDto;
using SoapCreateMobDto = BestiaryApi.Infrastructure.SOAP.Dtos.CreateMobDto;
using MinecraftMobs_Api.Dtos;

namespace BestiaryApi.Mappers;

public static class MobMapper
{

    //todo: Crear el endpoint de update en la api SOAP
    public static UpdateMobDto ToUpdateRequest(this Mob mob)
    {
        return new UpdateMobDto
        {
            Id = mob.Id,
            Name = mob.Name ?? "Default",
            Variant = mob.Variant ?? "Default",
            Behaviour = mob.Behaviour ?? "Default",
            Stats = new StatsDto
            {
                Health = mob.Stats.Health,
                Attack = mob.Stats.Attack,
                Armor = mob.Stats.Armor,
                Speed = mob.Stats.Speed
            }
        };
    }

    public static Mob ToModel(this UpdateMobRequest mob, Guid id)
    {
        return new Mob
        {
            Id = id,
            Name = mob.Name,
            Variant = mob.Variant,
            Behaviour = mob.Behaviour,
            Stats = new Stats
            {
                Health = mob.Stats.Health ?? 0,
                Attack = mob.Stats.Attack ?? 0,
                Armor = mob.Stats.Armor ?? 0,
                Speed = mob.Stats.Speed ?? 0
            }
        };
    }

    public static Mob ToModel(this SoapMobResponseDto mobResponseDto)
    {
        return new Mob
        {
            Id = mobResponseDto.Id,
            Name = mobResponseDto.Name,
            Variant = mobResponseDto.Variant,
            Behaviour = mobResponseDto.Behaviour,
            Stats = new Stats
            {
                Health = mobResponseDto.Stats.Health,
                Attack = mobResponseDto.Stats.Attack,
                Armor = mobResponseDto.Stats.Armor,
                Speed = mobResponseDto.Stats.Speed
            }
        };
    }

    public static Mob ToModel(this CreateMobRequest createMobRequest)
    {
        return new Mob
        {
            Name = createMobRequest.Name,
            Variant = createMobRequest.Variant,
            Behaviour = createMobRequest.Behaviour,
            Stats = new Stats
            {
                Health = createMobRequest.Stats.Health ?? 0,
                Attack = createMobRequest.Stats.Attack ?? 0,
                Armor = createMobRequest.Stats.Armor ?? 0,
                Speed = createMobRequest.Stats.Speed ?? 0
            }
        };
    }

    public static IList<Mob> ToModel(this IList<SoapMobResponseDto> mobResponseDtos)
    {
        return mobResponseDtos.Select(s => s.ToModel()).ToList();
    }

    public static SoapCreateMobDto ToRequest(this Mob mob)
    {
        return new SoapCreateMobDto
        {
            Name = mob.Name,
            Variant = mob.Variant,
            Behaviour = mob.Behaviour,
            Stats = new StatsDto
            {
                Health = mob.Stats.Health,
                Attack = mob.Stats.Attack,
                Armor = mob.Stats.Armor,
                Speed = mob.Stats.Speed
            }
        };
    }

    public static IList<RestMobResponse> ToResponse(this IList<Mob> mobs)
    {
        return mobs.Select(s => s.ToResponse()).ToList();
    }

    public static MobResponse ToResponse(this Mob mob)
    {
        return new MobResponse
        {
            Id = mob.Id,
            Name = mob.Name,
            Stats = new StatsRequest
            {
                Health = mob.Stats.Health,
                Attack = mob.Stats.Attack,
                Armor = mob.Stats.Armor,
                Speed = mob.Stats.Speed
            }
        };
    }
}
