using System.Data;
using MediatR;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.GetUsers;

[UnitOfWork(IsolationLevel.ReadCommitted)]
public sealed record GetUsersQuery(
    string CurrentUserUsername,
    string? FirstNamePrefix,
    string? LastNamePrefix
) : IRequest<GetUsersQueryResult>;