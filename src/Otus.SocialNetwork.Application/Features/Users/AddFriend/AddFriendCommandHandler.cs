using MediatR;
using Otus.SocialNetwork.Application.Exceptions;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.AddFriend;

public sealed class AddFriendCommandHandler : IRequestHandler<AddFriendCommand>
{
    private readonly IGetUsersQueryObject _getUsersQueryObject;
    private readonly ISaveUserQueryObject _saveUserQueryObject;

    public AddFriendCommandHandler(
        IGetUsersQueryObject getUsersQueryObject,
        ISaveUserQueryObject saveUserQueryObject)
    {
        _getUsersQueryObject = getUsersQueryObject;
        _saveUserQueryObject = saveUserQueryObject;
    }

    public async Task<Unit> Handle(AddFriendCommand request, CancellationToken ct)
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
                new() { Username = request.NewFriendUsername },
                ct);

        if (friendUser is null)
        {
            throw new UserNotFoundException(request.NewFriendUsername);
        }
        
        user.AddFriend(Friend.Create(friendUser));

        await _saveUserQueryObject.SaveAsync(user, ct);
        
        return Unit.Value;
    }
}