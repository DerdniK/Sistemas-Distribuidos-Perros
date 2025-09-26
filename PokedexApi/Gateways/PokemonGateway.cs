using PokedexApi.Dtos;
using PokedexApi.Models;
using PokedexApi.Infrastructure.Soap.Contracts;
using PokedexApi.Mappers;
using PokedexApi.Expections;
using System.ServiceModel;

namespace PokedexApi.Gateways;

public class PokemonGateway : IPokemonGateway
{
    private readonly IPokemonContract _pokemonContract;
    private readonly ILogger<PokemonGateway> _logger;

    public PokemonGateway(IConfiguration configuration, ILogger<PokemonGateway> logger)
    {
        var binding = new BasicHttpBinding();
        var endopoint = new EndpointAddress(configuration.GetValue<string>("PokemonService:Url"));
        _pokemonContract = new ChannelFactory<IPokemonContract>(binding, endopoint).CreateChannel();
        _logger = logger;
    }

    public async Task DeletePokemonAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _pokemonContract.DeletePokemon(id, cancellationToken);
        }
        catch (FaultException ex) when (ex.Message == "Pokemon not found")
        {
            _logger.LogWarning(ex, "Pokemon not found");
            throw new PokemonNotFoundException(id);    
        }
    }

    public async Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var pokemon = await _pokemonContract.GetPokemonById(id, cancellationToken);
            return pokemon.ToModel();
        }
        catch (FaultException ex) when (ex.Message == "Pokemon not found")
        {
            _logger.LogWarning(ex, "Pokemon Not Found");
            return null;
        }
    }

    public async Task<PokemonResponse> GetPokemonsByNameAsync(
        string name,
        string type,
        int pageNumber,
        int pageSize,
        string orderBy,
        string orderDirection,
        CancellationToken cancellationToken)
    {
        var allPokemons = await _pokemonContract.GetPokemonsByName(name, cancellationToken);

        var filtered = allPokemons
            .Where(p => string.IsNullOrEmpty(type) || p.Type.Contains(type, StringComparison.OrdinalIgnoreCase))
            .Select(p => p.ToModel())
            .ToList();

        filtered = orderBy.ToLower() switch
        {
            "name" => orderDirection.ToLower() == "desc" ? filtered.OrderByDescending(p => p.Name).ToList() : filtered.OrderBy(p => p.Name).ToList(),
            "type" => orderDirection.ToLower() == "desc" ? filtered.OrderByDescending(p => p.Type).ToList() : filtered.OrderBy(p => p.Type).ToList(),
            "level" => orderDirection.ToLower() == "desc" ? filtered.OrderByDescending(p => p.Level).ToList() : filtered.OrderBy(p => p.Level).ToList(),
            "attack" => orderDirection.ToLower() == "desc" ? filtered.OrderByDescending(p => p.Stats.Attack).ToList() : filtered.OrderBy(p => p.Stats.Attack).ToList(),
            _ => filtered.OrderBy(p => p.Name).ToList()
        };

        var totalRecords = filtered.Count;
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        var paged = filtered.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        var response = new PokemonResponse
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            Data = paged.Select(p => new PokemonResponseItem
            {
                Name = p.Name,
                Type = p.Type,
                Level = p.Level,
                Attack = p.Stats.Attack
            }).ToList()
        };

        return response;
    }

    public async Task<IList<Pokemon>> GetPokemonsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var allPokemons = await _pokemonContract.GetPokemonsByName(name, cancellationToken);
        return allPokemons.Select(p => p.ToModel()).ToList();
    }

    public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Sending request to SOAP API, with pokemon: {name}", pokemon.Name);
            var createdPokemon = await _pokemonContract.CreatePokemon(pokemon.ToRequest(), cancellationToken);
            return createdPokemon.ToModel();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Algo trono en el create pokemon a soap");
            throw;
        }
    }
}
