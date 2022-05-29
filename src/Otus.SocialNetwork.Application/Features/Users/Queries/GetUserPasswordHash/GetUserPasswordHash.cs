using System.Data;
using MediatR;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.Queries.GetUserPasswordHash;

[UnitOfWork(IsolationLevel.ReadCommitted)]
public sealed record GetUserPasswordHash(string Username) : IRequest<GetUserPasswordHashResult>;