using Otus.SocialNetwork.Application.Features.Users.Queries.GetUsers.Dtos;

namespace Otus.SocialNetwork.Application.Features.Users.Queries.GetUsers;

public sealed record GetUsersQueryResult(IReadOnlyList<UserDto> Users);