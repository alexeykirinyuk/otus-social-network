using Newtonsoft.Json.Linq;

namespace Otus.SocialNetwork.Kafka.Consumers.Posts;

public sealed record KafkaMessage(
    Guid EventId,
    DateTime OccurredAt,
    string EventType,
    JObject Event
);