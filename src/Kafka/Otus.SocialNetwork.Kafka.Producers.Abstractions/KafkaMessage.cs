namespace Otus.SocialNetwork.Kafka.Producers.Abstractions;

public sealed record KafkaMessage<T>(
    Guid EventId,
    DateTime OccurredAt,
    string EventType,
    T Event
) where T : IKafkaPayload;