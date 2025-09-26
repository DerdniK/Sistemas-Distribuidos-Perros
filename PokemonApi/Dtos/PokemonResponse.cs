namespace PokemonApi.Dtos;

public class PokemonResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public IList<PokemonResponseDto> Data { get; set; } = new List<PokemonResponseDto>();
}