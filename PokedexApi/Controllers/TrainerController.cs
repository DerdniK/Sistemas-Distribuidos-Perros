using Microsoft.AspNetCore.Mvc;
using PokedexApi.Exceptions;
using PokedexApi.Infrastructure.Grpc;
using PokedexApi.Models;
using PokedexApi.Dtos;
using Medal = PokedexApi.Models.Medal;
using PokedexApi.Services;

namespace PokedexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TrainerController : ControllerBase
{
    private readonly ITrainerService _TrainerService;
    public TrainerController(ITrainerService trainerService)
    {
        _TrainerService = trainerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainerResponseDto>> GetTrainerByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            var trainer = await _TrainerService.GetByIdAsync(id, cancellationToken);
            return Ok(ToDto(trainer));
        }
        catch (TrainerNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainerResponseDto>>> GetTrainers([FromQuery] string name, CancellationToken cancellationToken)
    {
        try
        {
            var trainers = await _TrainerService.GetAllByNameAsync(name, cancellationToken);
            return Ok(ToDto(trainers));
        }
        catch (TrainerNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrainers([FromBody] List<CreateTrainerRequestDto> request, CancellationToken cancellationToken)
    {
        var trainers = ToModel(request);
        var (successCount, createdTrainers) = await _TrainerService.CreateTrainersAsync(trainers, cancellationToken);
        return Ok(new
        {
            SuccessCount = successCount,
            CreatedTrainers = ToDto(createdTrainers)
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrainer(string id, CancellationToken cancellationToken)
    {
        try
        {
            await _TrainerService.DeleteTrainerAsync(id, cancellationToken);
            return NoContent();
        }
        catch (TrainerNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (TrainerInvalidIdException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrainer(string id,
    [FromBody] UpdateTrainerRequestDto request,
    CancellationToken cancellationToken)
    {
        try
        {
            var trainer = ToModel(id, request);
            await _TrainerService.UpdateTrainerAsync(trainer, cancellationToken);
            return NoContent();
        }
        catch (TrainerNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (TrainerValidationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (TrainerAlreadyExistsException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }

    private static Trainer ToModel(string id, UpdateTrainerRequestDto request)
    {
        return new Trainer
        {
            Id = id,
            Name = request.Name,
            Age = request.Age,
            Birthdate = request.Birthdate,
            Medals = request.Medals.Select(m => new Medal
            {
                Region = m.Region,
                Type = Enum.Parse<Models.MedalType>(m.Type)
            }
            ).ToList()
        };
    }
    
    private static IEnumerable<Trainer> ToModel(List<CreateTrainerRequestDto> trainers)
    {
        return trainers.Select(s => new Trainer
        {
            Name = s.Name,
            Age = s.Age,
            Birthdate = s.Birthdate,
            Medals = s.Medals.Select(m => new Medal
            {
                Region = m.Region,
                Type = Enum.Parse<Models.MedalType>(m.Type)
            }
            ).ToList()
        }).ToList();
    }

    private static IEnumerable<TrainerResponseDto> ToDto(IEnumerable<Trainer> trainers)
    {
        return trainers.Select(ToDto).ToList();
    }

    private static TrainerResponseDto ToDto(Trainer trainer)
    {
        return new TrainerResponseDto
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Age = trainer.Age,
            BirthDate = trainer.Birthdate,
            CreatedAt = trainer.CreatedAt,
            Medals = trainer.Medals.Select(ToDto).ToList()
        };
    }

    private static MedalDto ToDto(Medal medal)
    {
        return new MedalDto
        {
            Region = medal.Region,
            Type = medal.Type.ToString()
        };
    }
}