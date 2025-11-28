namespace BestiaryApi.Dtos;

public class MobResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public StatsRequest? Stats{ get; set; }
}