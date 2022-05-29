using MediatR;
using Newtonsoft.Json;
using Otus.SocialNetwork.Domain.Events;
using Otus.SocialNetwork.Kafka.Producers.Abstractions;
using Otus.SocialNetwork.Kafka.Producers.Abstractions.Posts;
using Otus.SocialNetwork.Kafka.Producers.Abstractions.Posts.Payloads;

namespace Otus.SocialNetwork.Application.Features.Posts.EventHandlers.PostPublished;

public sealed class SendPostPublishedKafkaEventHandler : INotificationHandler<PostPublishedEvent>
{
    private readonly IPostKafkaProducer _postKafkaProducer;

    public SendPostPublishedKafkaEventHandler(IPostKafkaProducer postKafkaProducer)
    {
        _postKafkaProducer = postKafkaProducer;
    }

    public async Task Handle(PostPublishedEvent domainEvent, CancellationToken ct)
    {
        var payload = new PostPublishedKafkaPayload(
            domainEvent.PostId,
            domainEvent.Username,
            domainEvent.PostText,
            domainEvent.PublishedAt);

        var kafkaMsg = new KafkaMessage<PostPublishedKafkaPayload>(
            domainEvent.EventId,
            domainEvent.OccurredAt,
            PostKafkaEventTypes.POST_PUBLISHED,
            payload);

        var key = domainEvent.PostId.ToString();
        var message = JsonConvert.SerializeObject(kafkaMsg);

        await _postKafkaProducer.Send(
            key,
            message,
            ct);
    }
}