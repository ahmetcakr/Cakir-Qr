using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Pipelines.RateLimiting;
public class RateLimitingConfiguration : RateLimitConfiguration
{
    private IServiceProvider Services { get; }

    public RateLimitingConfiguration(IOptions<IpRateLimitOptions> ipOptions, IOptions<ClientRateLimitOptions> clientOptions) : base(ipOptions, clientOptions)
    {
        Services = new ServiceCollection().BuildServiceProvider();
    }

    public override void RegisterResolvers()
    {
        // IP rate limit resolver
        var ipPolicyStore = Services.GetRequiredService<IIpPolicyStore>();
        var clientPolicyStore = Services.GetRequiredService<IClientPolicyStore>();

        // IP resolvers
        var ipResolvers = Services.GetRequiredService<IEnumerable<IRateLimitConfiguration>>();
        foreach (var resolver in ipResolvers)
        {
            resolver.RegisterResolvers();
        }

    }
}
