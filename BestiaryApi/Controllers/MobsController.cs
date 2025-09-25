using BestiaryApi.Dtos;
using BestiaryApi.Mappers;
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

    // localhost:PORT/api/v1/Mobs/ID(<- Es el ID del recurso)
    [HttpGet("{id}", Name = "GetMobByIdAsync")] // Es un endopoint del controlador y escucha un metodo Get, {id} es el recurso que va a viajar en la peticion
    public async Task<ActionResult<MobResponse>> GetMobByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var Mob = await _MobService.GetMobByIdAsync(id, cancellationToken);
        // HTTP verb - GET
        return Mob is null ? NotFound() : Ok(Mob.ToResponse()); // Regresa un status code en caso de que existe el Mob
        // En caso de que no existe se regresa un 404 (NotFound)
    }


    // localhost:port/api/v1/Mobs
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
    
}