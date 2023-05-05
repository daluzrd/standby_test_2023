namespace Api.Configuration.Security;

public record CorsConfig
{
    public CorsPolicyConfig[] Policies { get; init; } = Array.Empty<CorsPolicyConfig>();
}
