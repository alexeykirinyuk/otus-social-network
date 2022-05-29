using MediatR;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions.Users;

namespace Otus.SocialNetwork.Application.Features.Feed.AddPostToNewsFeed;

public sealed class AddPostToNewsFeedCommandHandler : IRequestHandler<AddPostToNewsFeedCommand>
{
    private readonly IGetSubscribersQueryObject _getSubscribers;

    public AddPostToNewsFeedCommandHandler(
        IGetSubscribersQueryObject getSubscribers)
    {
        _getSubscribers = getSubscribers;
    }

    public async Task<Unit> Handle(AddPostToNewsFeedCommand command, CancellationToken ct)
    {
        var subscribers = await _getSubscribers
            .GetUsernames(command.Username, ct);

        if (subscribers.Any())
        {
            return Unit.Value;
        }

        var newsFeed = subscribers
            .Select(subscriber => NewsFeedItem.Create(
                command.PostId,
                subscriber,
                command.Username,
                command.PostText,
                command.PublishedAt)
            )
            .ToArray();

        return Unit.Value;
    }
}