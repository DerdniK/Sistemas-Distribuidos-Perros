using System.ServiceModel;
using BestiaryApi.Expections;
using BestiaryApi.Mappers;
using BestiaryApi.Models;
using MinecraftMobs_Api.Contracts;


namespace BestiaryApi.Gateways;

public class MobGateway : IMobGateway
{
    private readonly IMobService _mobService;
    private readonly ILogger<MobGateway> _logger;
    private readonly List<Mob> _mobs = new();

    public MobGateway(IConfiguration configuration, ILogger<MobGateway> logger)
    {
        var binding = new BasicHttpBinding();
        var endpoint = new EndpointAddress(configuration.GetValue<string>("MobService:Url"));
        _mobService = new ChannelFactory<IMobService>(binding, endpoint).CreateChannel();
        _logger = logger;
    }
    
    public Task<IList<Mob>> GetAllMobsAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IList<Mob>>(_mobs);
    }



    public async Task UpdateMobAsync(Mob mob, CancellationToken cancellationToken)
    {
        try
        {
            await _mobService.UpdateMob(mob.ToUpdateRequest(), cancellationToken);
        }
        catch (FaultException ex) when (ex.Message == "Mob not found")
        {

            throw new MobNotFoundException(mob.Id);
        }
        catch (FaultException ex) when (ex.Message == "Mob with the same name already exists")
        {
            throw new MobAlreadyExistsException(mob.Name);
        }
    }

    public async Task DeleteMobAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mobService.DeleteMob(id, cancellationToken);
        }
        catch (FaultException ex) when (ex.Message == "Mob not found")
        {
            _logger.LogWarning(ex, "Mob not found");
            throw new MobNotFoundException(id);
        }
    }

    public async Task<Mob> GetMobByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var mob = await _mobService.GetMobById(id, cancellationToken);
            return mob.ToModel();
        }
        catch (FaultException ex) when (ex.Message == "Mob not found")
        {
            _logger.LogWarning(ex, "Mob not found");
            return null;
        }
    }

   //todo: Hacer el endpoint de getmobsbybehaviour y arrojar una lista de mobs.

    public async Task<IList<Mob>> GetMobsByBehaviourAsync(string name, CancellationToken cancellationToken)
    {
        _logger.LogDebug(":(");
        var mobs = await _mobService.GetMobsByBehaviour(name, cancellationToken);
        return mobs.ToModel();
    }

    public async Task<Mob> CreateMobAsync(Mob mob, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Sending request to SOAP API, with Mob: {name}", mob.Name);
            var createdMob = await _mobService.CreateMob(mob.ToRequest(), cancellationToken);
            return createdMob.ToModel();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Algo trono en el createMob a soap");
            throw;
        }
    }
}