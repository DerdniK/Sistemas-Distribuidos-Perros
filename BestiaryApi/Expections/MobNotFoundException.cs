namespace BestiaryApi.Expections;

public class MobNotFoundException : Exception
{
    public MobNotFoundException(Guid id) : base($"Mob {id} not found")
    {
        
    }
}