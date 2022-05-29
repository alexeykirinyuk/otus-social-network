using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Otus.SocialNetwork.Kafka.Producers.Abstractions.Posts;

namespace Otus.SocialNetwork.Kafka.Producers.Posts;

public sealed class PostKafkaProducer : IPostKafkaProducer
{
    private readonly ILogger<PostKafkaProducer> _logger;
    private readonly PostKafkaProducerOptions _opts;

    public PostKafkaProducer(
        IOptions<PostKafkaProducerOptions> opts,
        ILogger<PostKafkaProducer> logger)
    {
        _logger = logger;
        _opts = opts.Value;
    }

    public async Task Send(string key, string message, CancellationToken ct)
    {
        using var producer = new ProducerBuilder<string, string>(_opts.KafkaOptions).Build();
        try
        {
            await producer.ProduceAsync(
                _opts.Topic,
                new Message<string, string>
                {
                    Key = key,
                    Value = message
                }, ct);
        }
        catch (Exception e)
        {
            _logger.LogError(
                e,
                "Error when send kafka event. Key - '{key}', Message - '{message}'",
                key,
                message);
        }
    }
}