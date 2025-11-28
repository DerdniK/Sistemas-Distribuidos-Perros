using System.ComponentModel.DataAnnotations;

namespace BestiaryApi.Dtos;

public class StatsRequest
{
    [MaxLength(2)]
    public int Health { get; set; }

    [MaxLength(2)]
    public int Attack { get; set; }

    [MaxLength(2)]
    public int Armor { get; set; }
    
    [MaxLength(2)]
    public int Speed{ get; set; }
}