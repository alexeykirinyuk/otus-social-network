namespace Otus.SocialNetwork.Kafka.Producers.Abstractions.Posts;

public interface IPostKafkaProducer
{
    Task Send(
        string key,
        string message,
        CancellationToken ct);
}