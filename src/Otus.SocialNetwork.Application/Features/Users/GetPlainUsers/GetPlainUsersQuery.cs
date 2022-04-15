using System.Data;
using MediatR;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.GetPlainUsers;

[UnitOfWork(IsolationLevel.ReadCommitted)]
public sealed record GetPlainUsersQuery(
    string? FirstNamePrefix,
    string? LastNamePrefix,
    long? Offset,
    int? Limit
) : IRequest<GetPlainUsersQueryResult>;