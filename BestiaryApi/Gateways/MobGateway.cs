using System.ServiceModel;
using BestiaryApi.Infrastructure.SOAP.Contracts;
using BestiaryApi.Mappers;
using BestiaryApi.Models;

namespace BestiaryApi.Gateways;

public class MobGateway : IMobGateway
{
    private readonly IMobService _mobService;
    private readonly ILogger<MobGateway> _logger;

    public MobGateway(IConfiguration configuration, ILogger<MobGateway> logger)
    {
        var binding = new BasicHttpBinding();
        var endpoint = new EndpointAddress(configuration.GetValue<string>("MobService:Url"));
        _mobService = new ChannelFactory<IMobService>(binding, endpoint).CreateChannel();
        _logger = logger;
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
}