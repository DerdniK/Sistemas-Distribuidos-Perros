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

    public async Task<Pokemon> PatchPokemonAsync(Guid id, string? name, string? type, int? attack, int? defense, int? speed, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonGateway.GetPokemonByIdAsync(id, cancellationToken);
        if (pokemon is null)
        {
            throw new PokemonNotFoundException(id);
        }

        pokemon.Name = name ?? pokemon.Name;
        pokemon.Type = type ?? pokemon.Type;
        pokemon.Stats.Attack = attack ?? pokemon.Stats.Attack;
        pokemon.Stats.Defense = defense ?? pokemon.Stats.Defense;
        pokemon.Stats.Speed = speed ?? pokemon.Stats.Speed;

        await _pokemonGateway.UpdatePokemonAsync(pokemon, cancellationToken);
        return pokemon;
    }

    public async Task UpdatePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken)
    {
        await _pokemonGateway.UpdatePokemonAsync(pokemon, cancellationToken);
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
