namespace Otus.SocialNetwork.Persistence.QueryObjects.GetPlainUsers;

internal static class GetPlainUsersSql
{
    public const string GET_USERS = @"
SELECT username, first_name, last_name, date_of_birth, sex, city_id, password_hash, password_salt, created_at
FROM user
{0}
{1}
";
}