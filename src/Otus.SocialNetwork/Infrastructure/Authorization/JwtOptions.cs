namespace Otus.SocialNetwork.Infrastructure.Authorization;

public sealed class JwtOptions
{
    public const string JWT = "jwt";

    public string Secret { get; set; } = null!;
}