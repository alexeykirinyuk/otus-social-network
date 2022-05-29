using MediatR;
using Otus.SocialNetwork.Application.Exceptions;
using Otus.SocialNetwork.Persistence.Abstranctions.Users;

namespace Otus.SocialNetwork.Application.Features.Users.Commands.StopBeingFriends;

public sealed class StopBeingFriendsCommandHandler : IRequestHandler<StopBeingFriendsCommand>
{
    private readonly IGetUsersQueryObject _getUsersQueryObject;
    private readonly ISaveUserQueryObject _saveUserQueryObject;

    public StopBeingFriendsCommandHandler(
        IGetUsersQueryObject getUsersQueryObject,
        ISaveUserQueryObject saveUserQueryObject)
    {
        _getUsersQueryObject = getUsersQueryObject;
        _saveUserQueryObject = saveUserQueryObject;
    }

    public async Task<Unit> Handle(StopBeingFriendsCommand request, CancellationToken ct)
    {
        var user = await _getUsersQueryObject
            .SingleOrDefaultAsync(
                new() { Username = request.Username },
                ct
            );

        if (user is null)
        {
            throw new UserNotFoundException(request.Username);
        }

        var friendUser = await _getUsersQueryObject
            .SingleOrDefaultAsync(
                new() { Username = request.FriendUsername },
                ct);

        if (friendUser is null)
        {
            throw new UserNotFoundException(request.FriendUsername);
        }

        user.StopBeingFriends(friendUser);

        await _saveUserQueryObject.SaveAsync(user, ct);
        return Unit.Value;
    }
}