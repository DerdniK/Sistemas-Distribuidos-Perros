using System.Collections.Generic;
using System.Linq;
using PokedexApi.Dtos; // REST DTOs
using PokedexApi.Infrastructure.Soap.Dtos; // SOAP DTOs
using PokedexApi.Models;

// Alias para evitar ambigüedad
using RestPokemonResponse = PokedexApi.Dtos.PokemonResponse;
using SoapPokemonResponseDto = PokedexApi.Infrastructure.Soap.Dtos.PokemonResponseDto;
using SoapCreatePokemonDto = PokedexApi.Infrastructure.Soap.Dtos.CreatePokemonDto;

namespace PokedexApi.Mappers;

public static class PokemonMapper
{
    public static UpdatePokemonDto ToUpdateRequest(this Pokemon pokemon)
    {
        return new UpdatePokemonDto
        {
             Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Stats = new StatsDto
            {
                Attack = pokemon.Stats.Attack,
                Defense = pokemon.Stats.Defense,
                Speed = pokemon.Stats.Speed
            }
        };
    }
    public static Pokemon ToModel(this UpdatePokemonRequest pokemon, Guid id)
    {
        return new Pokemon
        {
            Id = id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Stats = new Stats
            {
                Attack = pokemon.Stats.Attack,
                Defense = pokemon.Stats.Defense,
                Speed = pokemon.Stats.Speed
            }
        };
    }
    public static Pokemon ToModel(this SoapPokemonResponseDto pokemonResponseDto)
    {
        return new Pokemon
        {
            Id = pokemonResponseDto.Id,
            Name = pokemonResponseDto.Name,
            Type = pokemonResponseDto.Type,
            Level = pokemonResponseDto.Level,
            Stats = new Stats
            {
                Attack = pokemonResponseDto.Stats.Attack,
                Defense = pokemonResponseDto.Stats.Defense,
                Speed = pokemonResponseDto.Stats.Speed
            }
        };
    }

    public static PokemonResponseItem ToResponseItem(this Pokemon pokemon)
{
    return new PokemonResponseItem
    {
        Name = pokemon.Name,
        Type = pokemon.Type,
        Level = pokemon.Level,
        Attack = pokemon.Stats.Attack,
        Defense = pokemon.Stats.Defense,
        Speed = pokemon.Stats.Speed
    };
}



    public static RestPokemonResponse ToResponse(this Pokemon pokemon)
{
    return new RestPokemonResponse
    {
        PageNumber = 1,
        PageSize = 1,
        TotalRecords = 1,
        TotalPages = 1,
        Data = new List<PokemonResponseItem>
        {
            pokemon.ToResponseItem()
        }
    };
}


    public static Pokemon ToModel(this CreatePokemonRequest createPokemonRequest)
    {
        return new Pokemon
        {
            Name = createPokemonRequest.Name,
            Type = createPokemonRequest.Type,
            Level = createPokemonRequest.Level,
            Stats = new Stats
            {
                Attack = createPokemonRequest.Stats.Attack,
                Defense = createPokemonRequest.Stats.Defense,
                Speed = createPokemonRequest.Stats.Speed
            }
        };
    }

    public static IList<Pokemon> ToModel(this IList<SoapPokemonResponseDto> pokemonResponseDtos)
    {
        return pokemonResponseDtos.Select(s => s.ToModel()).ToList();
    }

    public static SoapCreatePokemonDto ToRequest(this Pokemon pokemon)
    {
        return new SoapCreatePokemonDto
        {
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level,
            Stats = new StatsDto
            {
                Attack = pokemon.Stats.Attack,
                Defense = pokemon.Stats.Defense,
                Speed = pokemon.Stats.Speed
            }
        };
    }

    public static IList<RestPokemonResponse> ToResponse(this IList<Pokemon> pokemons)
    {
        return pokemons.Select(s => s.ToResponse()).ToList();
    }
}
