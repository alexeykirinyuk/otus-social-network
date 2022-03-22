using MediatR;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.GetUserPasswordHash;

public sealed class GetUserPasswordHashCommandHandler : IRequestHandler<GetUserPasswordHash, GetUserPasswordHashResult>
{
    private readonly IGetUserPasswordHashQueryObject _getUserPasswordHashQueryObject;

    public GetUserPasswordHashCommandHandler(
        IGetUserPasswordHashQueryObject getUserPasswordHashQueryObject)
    {
        _getUserPasswordHashQueryObject = getUserPasswordHashQueryObject;
    }

    public async Task<GetUserPasswordHashResult> Handle(GetUserPasswordHash request, CancellationToken ct)
    {
        var passwordHash = await _getUserPasswordHashQueryObject.GetPasswordHash(request.Username, ct);
        return new(passwordHash.HasValue, passwordHash?.Hash, passwordHash?.Salt);
    }
}