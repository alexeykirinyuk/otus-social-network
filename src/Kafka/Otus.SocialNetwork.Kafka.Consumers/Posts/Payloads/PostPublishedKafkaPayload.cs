namespace Otus.SocialNetwork.Kafka.Consumers.Posts.Payloads;

public sealed record PostPublishedKafkaPayload(
    Guid PostId,
    string Username,
    string PostText,
    DateTime PostPublishedAt
);