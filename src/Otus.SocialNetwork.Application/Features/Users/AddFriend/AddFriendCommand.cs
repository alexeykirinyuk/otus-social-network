using System.Data;
using MediatR;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.AddFriend;

[UnitOfWork(IsolationLevel.ReadCommitted)]
public sealed record AddFriendCommand(string Username, string NewFriendUsername) : IRequest;