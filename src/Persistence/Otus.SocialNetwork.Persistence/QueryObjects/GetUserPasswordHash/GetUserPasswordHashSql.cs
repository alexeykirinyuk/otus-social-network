namespace Otus.SocialNetwork.Persistence.QueryObjects.GetUserPasswordHash;

public static class GetUserPasswordHashSql
{
    public const string GET_USER_PASSWORD_HASH_BY_USERNAME = @"
    SELECT password_hash, password_salt
    FROM user
    WHERE username = @username
";
}