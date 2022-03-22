namespace Otus.SocialNetwork.Application.Features.Users.GetUserPasswordHash;

public sealed record GetUserPasswordHashResult(bool UserExists, string? Hash, string? Salt);