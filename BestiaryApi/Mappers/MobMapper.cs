using BestiaryApi.Dtos;
using BestiaryApi.Infrastructure.SOAP.Dtos;
using BestiaryApi.Models;

namespace BestiaryApi.Mappers;

public static class MobMapper
{
    public static Mob ToModel(this MobResponseDto mobResponseDto)
    {
        return new Mob
        {
            Id = mobResponseDto.Id,
            Name = mobResponseDto.Name,
            Stats = new Stats
            {
                Health = mobResponseDto.Stats.Health,
                Attack = mobResponseDto.Stats.Attack,
                Armor = mobResponseDto.Stats.Armor,
                Speed = mobResponseDto.Stats.Speed
            }
        };
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
