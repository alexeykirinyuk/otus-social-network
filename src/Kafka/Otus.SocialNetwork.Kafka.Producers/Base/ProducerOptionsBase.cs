namespace Otus.SocialNetwork.Kafka.Producers.Base;

public class ProducerOptionsBase
{
    public string Topic { get; set; } = string.Empty;

    public Dictionary<string, string> KafkaOptions { get; set; } = new();
}