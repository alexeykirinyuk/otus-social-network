using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Otus.SocialNetwork.Kafka.Consumers.Base;

public abstract class ConsumerBase
{
    private readonly ILogger<ConsumerBase> _logger;
    private readonly ConsumerOptionsBase _options;

    public ConsumerBase(
        ILogger<ConsumerBase> logger,
        ConsumerOptionsBase options)
    {
        _logger = logger;
        _options = options;
    }

    public async Task ConsumeAsync(CancellationToken ct)
    {
        using var consumer = CreateConsumer();
        consumer.Subscribe(_options.Topic);

        while (!ct.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(ct);
            var message = consumeResult.Message.Value;
            if (message is null)
            {
                continue;
            }

            await ConsumeMessageAsync(
                consumeResult.Message.Key,
                consumeResult.Message.Value,
                ct);

            consumer.Commit(consumeResult);
        }

        consumer.Close();
    }

    protected abstract Task ConsumeMessageAsync(
        string key,
        string message,
        CancellationToken ct);

    private IConsumer<string, string> CreateConsumer()
    {
        return new ConsumerBuilder<string, string>(_options.KafkaOptions)
            .SetErrorHandler(
                (_, e) => _logger.LogError(
                    "[{Topic}] Consumer error: {ErrorCode},{ErrorReason}",
                    _options.Topic,
                    e.Code,
                    e.Reason))
            .SetPartitionsAssignedHandler(
                (_, partitions) =>
                    _logger.LogInformation(
                        "[{Topic}] Assigned partitions: [{Partitions}]",
                        _options.Topic,
                        string.Join(", ", partitions))
            )
            .SetPartitionsRevokedHandler(
                (_, partitions) =>
                    _logger.LogInformation(
                        "[{Topic}] Revoking assignment: [{Partitions}]",
                        _options.Topic,
                        string.Join(", ", partitions))
            ).Build();
    }
}