using PokedexApi.Dtos;
using PokedexApi.Expections;
using PokedexApi.Gateways;
using PokedexApi.Models;

namespace PokedexApi.Services;

public class PokemonService : IPokemonService
{
    private readonly IPokemonGateway _pokemonGateway;

    public PokemonService(IPokemonGateway pokemonGateway)
    {
        _pokemonGateway = pokemonGateway;
    }

    public async Task DeletePokemonAsync(Guid id, CancellationToken cancellationToken)
    {
        await _pokemonGateway.DeletePokemonAsync(id, cancellationToken);
    }

    public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        var existing = await _pokemonGateway.GetPokemonsByNameAsync(pokemon.Name, cancellationToken);
        if (existing.Any(p => p.Name.Equals(pokemon.Name, StringComparison.OrdinalIgnoreCase)))
            throw new PokemonAlreadyExistsException(pokemon.Name);

        return await _pokemonGateway.CreatePokemonAsync(pokemon, cancellationToken);
    }

    public async Task<IList<Pokemon>> GetPokemonsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _pokemonGateway.GetPokemonsByNameAsync(name, cancellationToken);
    }

    public async Task<PokemonResponse> GetPokemonsByName(
        string name,
        string type,
        int pageNumber,
        int pageSize,
        string orderBy,
        string orderDirection,
        CancellationToken cancellationToken)
    {
        return await _pokemonGateway.GetPokemonsByNameAsync(name, type, pageNumber, pageSize, orderBy, orderDirection, cancellationToken);
    }

    public async Task<Pokemon> GetPokemonByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _pokemonGateway.GetPokemonByIdAsync(id, cancellationToken);
    }
}
