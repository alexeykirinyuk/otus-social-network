namespace Otus.SocialNetwork.Application.Features.Users.Queries.GetUserPasswordHash;

public sealed record GetUserPasswordHashResult(bool UserExists, string? Hash, string? Salt);