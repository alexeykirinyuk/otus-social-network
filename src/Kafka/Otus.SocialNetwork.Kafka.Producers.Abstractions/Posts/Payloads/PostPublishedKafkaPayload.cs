namespace Otus.SocialNetwork.Kafka.Producers.Abstractions.Posts.Payloads;

public sealed record PostPublishedKafkaPayload(
    Guid PostId,
    string Username,
    string PostText,
    DateTime PostPublishedAt
) : IKafkaPayload;