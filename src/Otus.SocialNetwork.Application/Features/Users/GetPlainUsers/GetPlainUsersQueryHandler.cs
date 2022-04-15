using MediatR;
using Otus.SocialNetwork.Application.Features.Users.GetPlainUsers.Dtos;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.GetPlainUsers;

public sealed class GetPlainUsersQueryHandler : IRequestHandler<GetPlainUsersQuery, GetPlainUsersQueryResult>
{
    private readonly IGetPlainUsersQueryObject _query;

    public GetPlainUsersQueryHandler(IGetPlainUsersQueryObject query)
    {
        _query = query;
    }

    public async Task<GetPlainUsersQueryResult> Handle(GetPlainUsersQuery request, CancellationToken cancellationToken)
    {
        var @params = new GetPlainUsersParams(
            request.FirstNamePrefix,
            request.LastNamePrefix,
            request.Limit,
            request.Offset);

        var users = await _query.GetListAsync(
            @params,
            cancellationToken);

        var userDtos = users
            .Select(user => new PlainUserDto(
                user.Username,
                user.FirstName,
                user.LastName,
                user.DateOfBirth,
                user.Sex,
                user.CityId,
                user.CreatedAt))
            .ToArray();

        return new(userDtos);
    }
}