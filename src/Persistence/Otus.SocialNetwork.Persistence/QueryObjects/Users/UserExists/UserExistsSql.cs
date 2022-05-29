namespace Otus.SocialNetwork.Persistence.QueryObjects.Users.UserExists;

public class UserExistsSql
{
    public const string CHECK_USER_EXISTS_BY_USERNAME = @"
SELECT EXISTS(
    SELECT 1
    FROM user
    WHERE username = @username
)
";
}