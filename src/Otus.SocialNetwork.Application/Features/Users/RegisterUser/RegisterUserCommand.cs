using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Application.Features.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Username,
    string? FirstName,
    string? LastName,
    DateTime? DateOfBirth,
    Sex? Sex,
    IReadOnlyList<string> Interests,
    string? City
);