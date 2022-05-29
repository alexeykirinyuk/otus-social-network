namespace Otus.SocialNetwork.Persistence.QueryObjects.Users.GetSubscribers;

public static class GetSubscribersSql
{
    public const string GET_SUBSCRIBER_USERNAMES = @"
SELECT user_username as username
FROM friend
WHERE friend_username IN @username
";
}