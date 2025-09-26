using System.ServiceModel;
using PokemonApi.Dtos;
using PokemonApi.Repositories;
using PokemonApi.Mappers;
using PokemonApi.Validators;
using PokemonApi.Models;
using Mysqlx.Crud;

namespace PokemonApi.Services;

public class PokemonService : IPokemonServices
{
    private readonly IPokemonRepository _pokemonRepository;
    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PokemonResponseDto> UpdatePokemon(UpdatePokemonDto pokemonToUpdate, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonByIdAsync(pokemonToUpdate.Id, cancellationToken);
        if (!PokemonExists(pokemon))
        {
            throw new FaultException("Pokemon not found");
        }

        // var duplicatedPokemon = await _pokemonRepository.GetByNameAsync(pokemonToUpdate.Name, cancellationToken);
        // duplicatedPokemon != null && duplicatedPokemon.Id != pokemonToUpdate.Id
        if (!await IsPokemonAllowedToBeUpdated(pokemonToUpdate, cancellationToken))
        {
            throw new FaultException("Pokemon with the same name already exists");
        }

        pokemon.Name = pokemonToUpdate.Name;
        pokemon.Type = pokemonToUpdate.Type;
        pokemon.stats.Attack = pokemonToUpdate.Stats.Attack;
        pokemon.stats.Defense = pokemonToUpdate.Stats.Defense;
        pokemon.stats.Speed = pokemonToUpdate.Stats.Speed;
        pokemon.stats.HP = pokemonToUpdate.Stats.HP;

        await _pokemonRepository.UpdatePokemonAsync(pokemon, cancellationToken);
        return pokemon.ToReponseDto();
    }

    private async Task<bool> IsPokemonAllowedToBeUpdated(UpdatePokemonDto pokemonToUpdate, CancellationToken cancellationToken)
    {
        var duplicatedPokemon = await _pokemonRepository.GetByNameAsync(pokemonToUpdate.Name, cancellationToken);
        //pokemon duplicado != no existe y es el mismo pokemon son falsos
        return duplicatedPokemon == null || IsTheSamePokemon(duplicatedPokemon, pokemonToUpdate);

    }

    private static bool IsTheSamePokemon(Pokemon pokemon, UpdatePokemonDto PokemonToUpdate)
    {
        // charizard != pikachu = true
        return pokemon.Id == PokemonToUpdate.Id;
    }

    public async Task<DeletePokemonResponseDto> DeletePokemon(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);
        if (!PokemonExists(pokemon))
        {
            throw new FaultException("Pokemon not found");
        }

        await _pokemonRepository.DeletePokemonAsync(pokemon, cancellationToken);
        return new DeletePokemonResponseDto { Succes = true };
    }

    public async Task<PokemonResponse> GetPokemonsByName(string name, string type, int pageNumber, int pageSize, string orderBy, string orderDirection, CancellationToken cancellationToken)
{
    var (pokemons, totalRecords) = await _pokemonRepository.GetPokemonsByNameAsync(
        name, type, pageNumber, pageSize, orderBy, orderDirection, cancellationToken);

    return new PokemonResponse
    {
        PageNumber = pageNumber,
        PageSize = pageSize,
        // TotalRecords = totalRecords,
        // TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
        Data = pokemons.ToResponseDto()
    };
}

    public async Task<PokemonResponseDto> GetPokemonById(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonByIdAsync(id, cancellationToken);
        return PokemonExists(pokemon) ? pokemon.ToReponseDto() : throw new FaultException("Pokemon not found");
    }
    public async Task<PokemonResponseDto> CreatePokemon(CreatePokemonDto pokemonRequest, CancellationToken cancellationToken)
    {
        pokemonRequest.ValidateName().ValidateLevel().ValidateType();
        if (await PokemonAlreadyExists(pokemonRequest.Name, cancellationToken))
        {
            throw new FaultException("Pokemon already exists");
        }

        var pokemon = await _pokemonRepository.CreateAsync(pokemonRequest.ToModel(), cancellationToken);


        return pokemon.ToReponseDto();
    }

    private static bool PokemonExists(Pokemon? pokemon)
    {
        return pokemon is not null; 
    }

    private async Task<bool> PokemonAlreadyExists(string name, CancellationToken cancellationToken)
    {
        var pokemons = await _pokemonRepository.GetByNameAsync(name, cancellationToken);
        return pokemons is not null;
    }
}