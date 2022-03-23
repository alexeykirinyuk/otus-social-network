using MediatR;

namespace Otus.SocialNetwork.Application.Features.Users.AddFriend;

public sealed record AddFriendCommand(string Username, string NewFriendUsername) : IRequest;