namespace BestiaryApi.Dtos;

public class UpdateMobRequest
{
    public string Name { get; set; }
    public string Variant { get; set; }
    public string Behaviour { get; set; }
    public StatsRequest Stats { get; set; }
}