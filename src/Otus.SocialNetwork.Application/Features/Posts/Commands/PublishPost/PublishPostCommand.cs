using MediatR;

namespace Otus.SocialNetwork.Application.Features.Posts.Commands.PublishPost;

public sealed record PublishPostCommand(
    string Username,
    string Text
) : IRequest;