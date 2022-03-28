using System.Data;
using MediatR;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.BeFriends;

[UnitOfWork(IsolationLevel.RepeatableRead)]
public sealed record BeFriendsCommand(string Username, string NewFriendUsername) : IRequest;