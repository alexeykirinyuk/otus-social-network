namespace Otus.SocialNetwork.Domain;

public sealed class NewsFeedItem
{
    public Guid PostId { get; }

    public string NewsFeedUsername { get; }

    public string PostPublisherUsername { get; }

    public string PostText { get; }

    public DateTime PostPublishedAt { get; }

    public NewsFeedItem(
        Guid postId,
        string newsFeedUsername,
        string postPublisherUsername,
        string postText,
        DateTime postPublishedAt)
    {
        PostId = postId;
        NewsFeedUsername = newsFeedUsername;
        PostPublisherUsername = postPublisherUsername;
        PostText = postText;
        PostPublishedAt = postPublishedAt;
    }

    public static NewsFeedItem Create(
        Guid postId,
        string newsFeedUsername,
        string postPublisherUsername,
        string postText,
        DateTime postPublishedAt)
    {
        return new(
            postId,
            newsFeedUsername,
            postPublisherUsername,
            postText,
            postPublishedAt
        );
    }
}