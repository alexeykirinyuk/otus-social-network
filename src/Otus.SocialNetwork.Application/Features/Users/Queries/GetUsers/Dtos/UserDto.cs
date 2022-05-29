using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Application.Features.Users.Queries.GetUsers.Dtos;

public sealed record UserDto(
    string Username,
    string? FirstName,
    string? LastName,
    DateTime? DateOfBirth,
    Sex? Sex,
    IReadOnlyList<string> Interests,
    string? City,
    DateTime CreatedAt,
    bool IsFriend
);