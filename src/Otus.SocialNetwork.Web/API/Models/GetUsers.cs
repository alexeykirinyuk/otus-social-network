namespace Otus.SocialNetwork.Web.API.Models;

public sealed class GetUsers
{
    public sealed record Request();

    public sealed record Response(IReadOnlyList<UserDto> Users);

    public sealed record UserDto(
        string Username,
        string? FirstName,
        string? LastName,
        DateTime? DateOfBirth,
        Sex? Sex,
        IReadOnlyList<string> Interests,
        string? City,
        DateTime CreatedAt,
        bool IsFriend
    );
}