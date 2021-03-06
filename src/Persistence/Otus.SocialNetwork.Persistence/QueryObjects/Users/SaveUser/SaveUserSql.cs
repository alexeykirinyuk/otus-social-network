namespace Otus.SocialNetwork.Persistence.QueryObjects.Users.SaveUser;

public static class SaveUsersSql
{
    public const string INSERT_OR_UPDATE_USER = @"
INSERT INTO user (username, first_name, last_name, date_of_birth, sex, city_id, password_hash, password_salt, created_at)
    VALUES (@username, @firstName, @lastName, @dateOfBirth, @sex, @cityId, @passwordHash, @passwordSalt, @createdAt)
ON DUPLICATE KEY UPDATE
first_name = @firstName,
last_name = @lastName,
date_of_birth = @dateOfBirth,
sex = @sex,
city_id = @cityId,
password_hash = @passwordHash,
password_salt = @passwordSalt,
created_at = @createdAt
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
WHERE user_interest.username = @username
";

    public const string DELETE_USER_INTERESTS = @"
DELETE FROM user_interest
WHERE username = @username AND interest_id IN @interestIds
";

    public const string GET_INTERESTS_BY_NAMES = @"
SELECT id, name
FROM interest
WHERE name IN @names
";

    public const string ADD_INTEREST = @"
INSERT INTO interest (name) VALUES(@name);
SELECT LAST_INSERT_ID();
";

    public const string ADD_USER_INTERESTS = @"
INSERT INTO user_interest (username, interest_id)
VALUES {0};
";

    public const string GET_FRIENDS_BY_USERNAME = @"
SELECT user_username, friend_username
FROM friend
WHERE user_username = @username
";

    public const string ADD_FRIENDS = @"
INSERT INTO friend (user_username, friend_username)
VALUES {0};
";
    
    public const string DELETE_USER_FRIENDS = @"
DELETE FROM friend
WHERE user_username = @username AND friend_username IN @friendUsernames;
";
}