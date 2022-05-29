namespace Otus.SocialNetwork.Persistence.QueryObjects.Users.SaveUserBulk;

public sealed class SaveUserBulkSql
{
    public const string INSERT_USERS_BULK = @"
INSERT INTO user (username, first_name, last_name, date_of_birth, sex, city_id, password_hash, password_salt, created_at)
    VALUES {0}
";
}