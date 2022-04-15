using Otus.SocialNetwork.Application.Features.Users.GetPlainUsers.Dtos;

namespace Otus.SocialNetwork.Application.Features.Users.GetPlainUsers;

public sealed record GetPlainUsersQueryResult(IReadOnlyList<PlainUserDto> Users);