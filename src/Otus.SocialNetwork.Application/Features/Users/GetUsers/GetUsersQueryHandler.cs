using MediatR;
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
        var users = await _getUsersQueryObject.ToListAsync(null, cancellationToken);

        var userDtos = users
            .Select(user => new UserDto(
                user.Username,
                user.FirstName,
                user.LastName,
                user.DateOfBirth,
                user.Sex,
                user.Interests,
                user.City,
                user.CreatedAt))
            .ToArray();

        return new(userDtos);
    }
}