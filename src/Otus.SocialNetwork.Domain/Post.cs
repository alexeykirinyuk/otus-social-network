using Otus.SocialNetwork.Domain.Events;
using Otus.SocialNetwork.Libs.SharedKernel;

namespace Otus.SocialNetwork.Domain;

public sealed class Post : AggregationRoot
{
    public Guid Id { get; }

    public string Username { get; }

    public string Text { get; }

    public DateTime PublishedAt { get; }

    public Post(
        Guid id,
        string username,
        string text,
        DateTime publishedAt)
    {
        Id = id;
        Username = username;
        Text = text;
        PublishedAt = publishedAt;
    }

    public static Post Create(
        string username,
        string text)
    {
        var post = new Post(
            Guid.NewGuid(),
            username,
            text,
            DateTime.UtcNow);

        post.AddDomainEvent(
            new PostPublishedEvent(
                post.Id,
                post.Username,
                post.Text,
                post.PublishedAt)
        );

        return post;
    }
}