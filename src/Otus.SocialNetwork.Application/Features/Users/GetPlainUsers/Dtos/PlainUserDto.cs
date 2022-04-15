using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.Application.Features.Users.GetPlainUsers.Dtos;

public sealed record PlainUserDto(
    string Username,
    string? FirstName,
    string? LastName,
    DateTime? DateOfBirth,
    Sex? Sex,
    long? CityId,
    DateTime CreatedAt
);