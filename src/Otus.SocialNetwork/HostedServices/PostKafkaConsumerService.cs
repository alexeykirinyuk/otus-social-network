using Otus.SocialNetwork.Kafka.Consumers.Posts;

namespace Otus.SocialNetwork.HostedServices;

public sealed class PostKafkaConsumerService : BackgroundService
{
    private readonly PostKafkaConsumer _postKafkaConsumer;

    public PostKafkaConsumerService(PostKafkaConsumer postKafkaConsumer)
    {
        _postKafkaConsumer = postKafkaConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _postKafkaConsumer.ConsumeAsync(stoppingToken);
    }
}