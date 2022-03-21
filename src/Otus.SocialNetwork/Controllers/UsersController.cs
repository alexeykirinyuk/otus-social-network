using MediatR;
using Microsoft.AspNetCore.Mvc;
using Otus.SocialNetwork.Application.Features.Users.RegisterUser;

namespace Otus.SocialNetwork.Controllers;

public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task RegisterNewUser([FromBody] RegisterUserCommand command, CancellationToken ct)
    {
        return _mediator.Send(command, ct);
    }
}