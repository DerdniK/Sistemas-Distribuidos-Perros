using System.ServiceModel;
using MinecraftMobs_Api.Dtos;

namespace MinecraftMobs_Api.Validators;

public static class MobValidator
{
    public static CreateMobDto ValidateName(this CreateMobDto mob) =>
        string.IsNullOrEmpty(mob.Name) ? throw new FaultException("Mob name is empty") : mob;

    public static CreateMobDto ValidateVariant(this CreateMobDto mob) =>
        string.IsNullOrEmpty(mob.Variant) ? throw new FaultException("Mob variant is empty") : mob;

    public static CreateMobDto ValidateBehaviour(this CreateMobDto mob) =>
        string.IsNullOrEmpty(mob.Behaviour) ? throw new FaultException("Mob behaviour is empty") : mob;

    public static CreateMobDto ValidateHealth(this CreateMobDto mob) =>
        mob.Stats.Health <= 0 ? throw new FaultException("Mob health not could be 0") : mob;

    public static CreateMobDto ValidateArmor(this CreateMobDto mob) =>
        mob.Stats.Armor <= 0 ? throw new FaultException("Mob Armor not could be 0") : mob;

    public static CreateMobDto ValidateAttack(this CreateMobDto mob) =>
        mob.Stats.Attack <= 0 ? throw new FaultException("Mob Attack not could be 0") : mob;

    public static CreateMobDto ValidateSpeed(this CreateMobDto mob) =>
        mob.Stats.Speed <= 0 ? throw new FaultException("Mob Speed not could be 0") : mob;
}