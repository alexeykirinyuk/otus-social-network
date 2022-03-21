namespace Otus.SocialNetwork.Persistence;

public static class UsersSql
{
    public const string CHECK_USER_EXISTS_BY_USERNAME = @"
SELECT EXISTS(
    SELECT 1
    FROM user
    WHERE username = @username
)
";
}