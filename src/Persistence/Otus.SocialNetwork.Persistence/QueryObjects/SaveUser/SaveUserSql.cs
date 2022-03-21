namespace Otus.SocialNetwork.Persistence.QueryObjects.SaveUser;

public static class SaveUsersSql
{
    public const string INSERT_OR_UPDATE_USER = @"
INSERT INTO user (username, first_name, last_name, date_of_birth, sex, city_id, created_at)
    VALUES (@username, @firstName, @lastName, @dateOfBirth, @sex, @cityId, @createdAt)
ON CONFLICT (username) DO UPDATE SET
first_name = EXCLUDED.first_name,
last_name = EXCLUDED.last_name,
date_of_birth = EXCLUDED.date_of_birth,
sex = EXCLUDED.sex,
city_id = EXCLUDED.city_id,
created_at = EXCLUDED.created_at
";

    public const string GET_CITY_ID_BY_NAME = @"
SELECT id
FROM city
WHERE name = @name
";

    public const string INSERT_CITY = @"
INSERT INTO city (name) VALUES (@name);
SELECT LAST_INSERT_ID();
";

    public const string GET_USER_INTERESTS = @"
SELECT interest.id, interest.name FROM user_interest
JOIN interest ON interest.id = user_interest.interest_id
WHERE username = @username
";

    public const string DELETE_USER_INTERESTS = @"
DELETE FROM user_interest
WHERE username = @username AND interest_id = ANY(@interestIds)
";

    public const string GET_INTERESTS_BY_NAMES = @"
SELECT id, name
FROM interest
WHERE name = ANY(@names)
";

    public const string ADD_INTEREST = @"
INSERT INTO interest (name) VALUES(@name);
SELECT LAST_INSERT_ID();
";

    public const string ADD_USER_INTERESTS = @"
INSERT INTO user_interest (username, interest_id)
{0}
";
}