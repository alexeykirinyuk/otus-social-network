namespace Otus.SocialNetwork.Persistence.QueryObjects.GetUsers;

internal static class GetUsersSql
{
    public const string GET_USERS = @"
SELECT username, first_name, last_name, date_of_birth, sex, city_id, password_hash, password_salt, created_at
FROM user
{0}
ORDER BY username DESC
";
    
    public const string GET_USERS_COUNT = @"
SELECT COUNT(1)
FROM user
";

    public const string GET_USER_INTERESTS_BY_USERNAMES = @"
SELECT username, interest_id
FROM user_interest
WHERE username IN @usernames
";

    public const string GET_CITIES_BY_IDS = @"
SELECT id, name
FROM city
WHERE id IN @cityIds
";

    public const string GET_INTERESTS_BY_IDS = @"
SELECT id, name
FROM interest
WHERE id IN @interestIds
";

    public const string GET_FRIENDS_BY_USERNAMES = @"
SELECT user_username, friend_username
FROM friend
WHERE user_username IN @usernames
";
}