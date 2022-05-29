using Otus.SocialNetwork.Libs.SharedKernel;

namespace Otus.SocialNetwork.Domain.Events;

public sealed record PostPublishedEvent(
    Guid PostId,
    string Username,
    string PostText,
    DateTime PublishedAt
    ) : DomainEvent;