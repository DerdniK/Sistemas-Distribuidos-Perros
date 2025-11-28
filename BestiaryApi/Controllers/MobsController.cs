using BestiaryApi.Dtos;
using BestiaryApi.Expections;
using BestiaryApi.Mappers;
using BestiaryApi.Models;
using BestiaryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BestiaryApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MobsController : ControllerBase // Nos va a dar status code verbos HTTP etc.
{
    private readonly IMobService _MobService;

    public MobsController(IMobService MobsService)
    {
        _MobService = MobsService;
    }

    [HttpGet]
    public async Task<ActionResult> GetMobsAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string orderBy = "Name",
        [FromQuery] string orderDirection = "asc",
        CancellationToken cancellationToken = default)
    {
        var mobs = await _MobService.GetAllMobsAsync(cancellationToken);

        // Ordenar (solo por algunos campos comunes)
        if (orderBy.ToLower() == "attack")
        {
            mobs = orderDirection.ToLower() == "desc"
                ? mobs.OrderByDescending(m => m.Stats.Attack).ToList()
                : mobs.OrderBy(m => m.Stats.Attack).ToList();
        }
        else
        {
            mobs = orderDirection.ToLower() == "desc"
                ? mobs.OrderByDescending(m => m.Name).ToList()
                : mobs.OrderBy(m => m.Name).ToList();
        }

        // Paginación
        var totalRecords = mobs.Count;
        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        var pagedData = mobs
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(m => m.ToResponse())
            .ToList();

        return Ok(new
        {
            pageNumber,
            pageSize,
            totalRecords,
            totalPages,
            data = pagedData
        });
    }




    // localhost:PORT/api/v1/Mobs/ID(<- Es el ID del recurso)
    [HttpGet("{id}", Name = "GetMobByIdAsync")] // Es un endopoint del controlador y escucha un metodo Get, {id} es el recurso que va a viajar en la peticion
    public async Task<ActionResult<MobResponse>> GetMobByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var Mob = await _MobService.GetMobByIdAsync(id, cancellationToken);
        // HTTP verb - GET
        if (Mob == null) return NotFound();
        return Mob is null ? NotFound() : Ok(Mob.ToResponse()); // Regresa un status code en caso de que existe el Mob
        // En caso de que no existe se regresa un 404 (NotFound)
    }


    // localhost:port/api/v1/mobs
    // Body request - JSON
    // HTTP verb POST
    // HTTP Status
    // 400 Bad request (Si el usuario manda informacion incorrecta)
    // 409 Conlict (Ya existe esa entidad)
    // 422 Entidad no procesable (Por alguna regla de negocio interna)
    // 500 Internal Error
    // 200 - Ok (El recurso creado + id) -- no sigue bien las buenas practicas de RESTFUL
    // 201 - Created (Recurso creado + id) -- Response header retorna un href donde referencia al GET para obtener el recurso
    // 202 - Accepted (Procesamiento async)
    [HttpPost]
    public async Task<ActionResult<MobResponse>> CreateMobAsync([FromBody] CreateMobRequest createMob, CancellationToken cancellationToken)
    {
        try
        {
            var mob = createMob.ToModel();
            var createdMob = await _MobService.CreateMobAsync(mob, cancellationToken);

            // Solo regresamos el objeto creado
            return Ok(createdMob.ToResponse());
        }
        catch (System.ServiceModel.FaultException ex) when (ex.Message.Contains("Mob already exists"))
        {
            return Conflict(new { Message = "Mob ya existe" });
        }
        catch
        {
            return StatusCode(500, new { Message = "Ocurrió un error inesperado" });
        }
    }



    //localhost:port/api/v1/mobs/ID
    // HTTP VERB - DELETE
    // 204 - No Content (si se borro correctamente)
    // 200 - Ok (Si se borro correctamente) - no sigue buenas practicas RESTFUL
    // {"message": "mob borrado correctamente"} - a veces regresan un JSON como respuesta
    // 404 - Not Found (Si el mob no existe)
    // 500 - Internal server error

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMobAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _MobService.DeleteMobAsync(id, cancellationToken);
            return NoContent(); // 204
        }
        catch (MobNotFoundException)
        {
            return NotFound(); // 404
        }
    }

    //localhost:port/api/v1/mobs/ID
    // HTTP verb - PUT
    // 204 - No Content(Orientado a restfull)
    // 200 - Ok(Retornar la entidad actualizada)
    // 404 - Not Found(Id no exite)
    // 400 - Validaciones de los campos incorrectos
    // 500 - Internal Server Error
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMobAsync(Guid id, [FromBody] UpdateMobRequest mob, CancellationToken cancellationToken)
    {
        try
        {
            if (!IsValidAttack(mob.Stats.Attack)) {
                return BadRequest(new{Message = "Invalid attack value"}); //400
            }
            if (string.IsNullOrWhiteSpace(mob.Behaviour))
            return BadRequest(new { Message = "Behaviour is required" });

            if (string.IsNullOrWhiteSpace(mob.Name))
            return BadRequest(new { Message = "Name is required" });


            await _MobService.UpdateMobAsync(mob.ToModel(id), cancellationToken);
            return NoContent(); // 204
        }
        catch (MobNotFoundException)
        {
            return NotFound(); // 404
        }
        catch (MobAlreadyExistsException ex)
        {
            return Conflict(new {Message = ex.Message}); // 409
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<MobResponse>> PatchMobAsync(Guid id, [FromBody] PatchMobRequest mobRequest, CancellationToken cancellationToken)
    {
        try
        {
            if (mobRequest.Stats is not null
            && mobRequest.Stats.Attack.HasValue
            && !IsValidAttack(mobRequest.Stats.Attack.Value))
            {
                return BadRequest(new { Message = "Invalid attack value" });
            }


            var mob = await _MobService.PatchMobAsync(id, mobRequest, cancellationToken);
            return Ok(mob.ToResponse()); // 204
        }
        catch (MobNotFoundException)
        {
            return NotFound(); // 404
        }
        catch (MobAlreadyExistsException ex)
        {
            return Conflict(new {Message = ex.Message}); // 409
        }
    }
    

    private static bool IsValidAttack(int? Attack)
    {
        return Attack >= 0;
    }
    
}