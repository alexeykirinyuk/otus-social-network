using MediatR;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Libs.SharedKernel;
using Otus.SocialNetwork.Persistence.Abstranctions.Posts;

namespace Otus.SocialNetwork.Application.Features.Posts.Commands.PublishPost;

public sealed class PublishPostCommandHandler : IRequestHandler<PublishPostCommand>
{
    private readonly ISavePostQueryObject _savePostQueryObject;
    private readonly IEventBus _eventBus;

    public PublishPostCommandHandler(
        ISavePostQueryObject savePostQueryObject,
        IEventBus eventBus)
    {
        _savePostQueryObject = savePostQueryObject;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(PublishPostCommand request, CancellationToken ct)
    {
        var post = Post.Create(request.Username, request.Text);
        await _savePostQueryObject.Save(post, ct);
        await _eventBus.PublishAllAsync(post.PopDomainEvents(), ct);
        
        return Unit.Value;
    }
}