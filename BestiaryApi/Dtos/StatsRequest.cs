using System.ComponentModel.DataAnnotations;

namespace BestiaryApi.Dtos;

public class StatsRequest
{
    [Range(0, 99)]
    public int? Health { get; set; }
    [Range(0, 99)]
    public int? Attack { get; set; }
    [Range(0, 99)]
    public int? Armor { get; set; }
    [Range(0, 99)]
    public int? Speed{ get; set; }
}