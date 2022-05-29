namespace Otus.SocialNetwork.Kafka.Consumers.Base;

public abstract class ConsumerOptionsBase
{
    public string Topic { get; set; } = string.Empty;

    public Dictionary<string, string> KafkaOptions { get; set; } = new();
}