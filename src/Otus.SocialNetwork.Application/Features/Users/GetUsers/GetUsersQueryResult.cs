using Otus.SocialNetwork.Application.Features.Users.GetUsers.Dtos;

namespace Otus.SocialNetwork.Application.Features.Users.GetUsers;

public sealed record GetUsersQueryResult(IReadOnlyList<UserDto> Users);