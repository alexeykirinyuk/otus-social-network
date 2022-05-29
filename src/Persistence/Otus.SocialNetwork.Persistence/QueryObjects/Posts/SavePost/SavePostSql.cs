namespace Otus.SocialNetwork.Persistence.QueryObjects.Posts.SavePost;

public static class SavePostSql
{
    public const string INSERT_OR_UPDATE_POST = @"
    INSERT INTO post (id, username, text, published_at)
    VALUES (@id, @username, @text, @publishedAt)
    ON DUPLICATE KEY UPDATE
    text = @text
";
}