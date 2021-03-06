using MediatR;
using Otus.SocialNetwork.Application.Exceptions;
using Otus.SocialNetwork.Application.Features.Users.Queries.GetUsers.Dtos;
using Otus.SocialNetwork.Persistence.Abstranctions.Users;

namespace Otus.SocialNetwork.Application.Features.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResult>
{
    private readonly IGetUsersQueryObject _getUsersQueryObject;

    public GetUsersQueryHandler(IGetUsersQueryObject getUsersQueryObject)
    {
        _getUsersQueryObject = getUsersQueryObject;
    }

    public async Task<GetUsersQueryResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _getUsersQueryObject.SingleOrDefaultAsync(
            new GetUsersParams { Username = request.CurrentUserUsername },
            cancellationToken);

        if (currentUser is null)
        {
            throw new UserNotFoundException(request.CurrentUserUsername);
        }

        var friendUsernames = currentUser.Friends
            .Select(friend => friend.Username)
            .ToHashSet();

        var @params = new GetUsersParams
        {
            FirstNamePrefix = request.FirstNamePrefix,
            LastNamePrefix = request.LastNamePrefix,
            Offset = request.Offset,
            Limit = request.Limit
        };
        var users = await _getUsersQueryObject.ToListAsync(
            @params,
            cancellationToken);

        var userDtos = users
            .Select(user => new UserDto(
                user.Username,
                user.FirstName,
                user.LastName,
                user.DateOfBirth,
                user.Sex,
                user.Interests,
                user.City,
                user.CreatedAt,
                friendUsernames.Contains(user.Username)))
            .ToArray();

        return new(userDtos);
    }
}