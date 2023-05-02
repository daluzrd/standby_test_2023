namespace Api.Configuration.Security
{
    public record CorsPolicyConfig
    {
        public string Name { get; init; } = string.Empty;
        public string[] AllowedOrigins { get; init; } = Array.Empty<string>();
        public string[] AllowedMethods { get; init; } = Array.Empty<string>();
        public string[] AllowedHeaders { get; init; } = Array.Empty<string>();
        public bool AllowWildcardSubdomains { get; init; } = false;
    }
}
