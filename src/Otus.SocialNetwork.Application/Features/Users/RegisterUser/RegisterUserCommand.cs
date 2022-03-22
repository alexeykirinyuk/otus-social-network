using System.Data;
using MediatR;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.RegisterUser;

[UnitOfWork(IsolationLevel.ReadCommitted)]
public sealed record RegisterUserCommand(
    string Username,
    string? FirstName,
    string? LastName,
    DateTime? DateOfBirth,
    Sex? Sex,
    IReadOnlyList<string> Interests,
    string? City,
    string PasswordHash,
    string PasswordSalt
) : IRequest;