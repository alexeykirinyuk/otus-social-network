using System.Data;
using MediatR;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.Commands.StopBeingFriends;

[UnitOfWork(IsolationLevel.RepeatableRead)]
public sealed record StopBeingFriendsCommand(string Username, string FriendUsername) : IRequest;