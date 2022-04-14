using MediatR;
using Otus.SocialNetwork.Application.Exceptions;
using Otus.SocialNetwork.Application.Features.Users.GetUsers.Dtos;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.GetUsers;

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
            new GetUsersFilters { Username = request.CurrentUserUsername },
            cancellationToken);

        if (currentUser is null)
        {
            throw new UserNotFoundException(request.CurrentUserUsername);
        }

        var friendUsernames = currentUser.Friends
            .Select(friend => friend.Username)
            .ToHashSet();

        var filters = new GetUsersFilters
        {
            FirstNamePrefix = request.FirstNamePrefix,
            LastNamePrefix = request.LastNamePrefix
        };
        var users = await _getUsersQueryObject.ToListAsync(
            filters,
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