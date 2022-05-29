using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Otus.SocialNetwork.Application.Features.Feed.AddPostToNewsFeed;
using Otus.SocialNetwork.Kafka.Consumers.Base;
using Otus.SocialNetwork.Kafka.Consumers.Posts.Payloads;

namespace Otus.SocialNetwork.Kafka.Consumers.Posts;

public sealed class PostKafkaConsumer : ConsumerBase
{
    private readonly IServiceProvider _serviceProvider;

    public PostKafkaConsumer(
        ILogger<PostKafkaConsumer> logger,
        PostKafkaConsumerOptions options,
        IServiceProvider serviceProvider
        ) : base(logger, options)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ConsumeMessageAsync(string key, string message, CancellationToken ct)
    {
        var kafkaMessage = JsonConvert.DeserializeObject<KafkaMessage>(message);
        if (kafkaMessage is null)
        {
            throw new InvalidOperationException($"Can't deserialize message: {message}");
        }

        switch (kafkaMessage.EventType)
        {
            case PostKafkaEventTypes.POST_PUBLISHED:
                await HandlePostPublishedEvent(kafkaMessage, ct);
                break;
        }
    }

    private async Task HandlePostPublishedEvent(KafkaMessage kafkaMessage, CancellationToken ct)
    {
        var payload = kafkaMessage.Event.ToObject<PostPublishedKafkaPayload>();
        if (payload is null)
        {
            throw new InvalidOperationException($"Can't deserialize payload.");
        }

        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(
            new AddPostToNewsFeedCommand(
                payload.PostId,
                payload.Username,
                payload.PostText,
                payload.PostPublishedAt),
            ct);
    }
}