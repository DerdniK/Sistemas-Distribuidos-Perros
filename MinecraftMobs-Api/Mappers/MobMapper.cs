using Microsoft.AspNetCore.StaticAssets;
using MinecraftMobs_Api.Infrastructure.Entities;
using MinecraftMobs_Api.Models;
using MinecraftMobs_Api.Dtos;

namespace MinecraftMobs_Api.Mappers;


public static class MobMapper
{
    //extension methods
    public static Mob? ToModel(this MobEntity mobEntity) //! Convierte de MobEntity a Mob (DataBase => API)
    {
        if (mobEntity is null)
        {
            return null;
        }

        return new Mob
        {
            Id = mobEntity.Id,
            Name = mobEntity.Name,
            Variant = mobEntity.Variant,
            Behaviour = mobEntity.Behaviour,
            Stats = new Stats
            {
                Health = mobEntity.Health,
                Attack = mobEntity.Attack,
                Armor = mobEntity.Armor,
                Speed = mobEntity.Speed
            }
        };
    }

    public static MobEntity ToEntity(this Mob mob) //! Convierte de Mob a MobEntity (API => DataBase)
    {
        return new MobEntity
        {
            Id = mob.Id,
            Name = mob.Name,
            Variant = mob.Variant,
            Behaviour = mob.Behaviour,
            Health = mob.Stats.Health,
            Attack = mob.Stats.Attack,
            Armor = mob.Stats.Armor,
            Speed = mob.Stats.Speed
        };
    }

    public static MobResponseDto ToResponseDto(this Mob mob) //! Convertir de Mob a MobResponseDto
    {
        return new MobResponseDto
        {
            Id = mob.Id,
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

    public static Mob ToModel(this CreateMobDto requestMobDto)
    {
        return new Mob
        {
            Name = requestMobDto.Name,
            Variant = requestMobDto.Variant,
            Behaviour = requestMobDto.Behaviour,
            Stats = new Stats
            {
                Health = requestMobDto.Stats.Health,
                Attack = requestMobDto.Stats.Attack,
                Armor = requestMobDto.Stats.Armor,
                Speed = requestMobDto.Stats.Speed
            }
        };
    }

    public static IList<Mob> ToModel(this IList<MobEntity> mobs)
    {
        return mobs.Select(e => e.ToModel()).ToList();
    }

    // public static IList<MobResponseDto> ToResponseDto(this IList<Mob> mobs)
    // {
    //     return mobs.Select(m => m.ToResponseDto()).ToList();
    // }


    public static IList<MobResponseDto> ToResponseDto(this IReadOnlyList<Mob> mobs)
    {
        return mobs.Select(s => s.ToResponseDto()).ToList();
    }
    
    public static IReadOnlyList<Mob> ToReadOnlyModel(this IReadOnlyList<MobEntity> mobs)
    {
        return mobs.Select(s => s.ToModel()).ToList();
    }
    }