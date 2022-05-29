using MediatR;

namespace Otus.SocialNetwork.Application.Features.Feed.AddPostToNewsFeed;

public sealed record AddPostToNewsFeedCommand(
    Guid PostId,
    string Username,
    string PostText,
    DateTime PublishedAt
    ) : IRequest;