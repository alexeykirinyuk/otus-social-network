using Otus.SocialNetwork.Domain;

namespace Otus.SocialNetwork.ViewModels;

public sealed record RegisterUserViewModel(
    string Username,
    string? FirstName,
    string? LastName,
    DateTime? DateOfBirth,
    Sex? Sex,
    IReadOnlyList<string> Interests,
    string? City,
    string Password
);