namespace BestiaryApi.Expections;

public class MobAlreadyExistsException : Exception
{
    public MobAlreadyExistsException(string mobName) : base($"Mob {mobName} already exists")
    {
        
    }
}