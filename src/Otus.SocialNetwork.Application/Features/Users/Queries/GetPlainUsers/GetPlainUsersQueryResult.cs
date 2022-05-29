using Otus.SocialNetwork.Application.Features.Users.Queries.GetPlainUsers.Dtos;

namespace Otus.SocialNetwork.Application.Features.Users.Queries.GetPlainUsers;

public sealed record GetPlainUsersQueryResult(IReadOnlyList<PlainUserDto> Users);