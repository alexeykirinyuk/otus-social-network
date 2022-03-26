using MediatR;
using Microsoft.AspNetCore.Mvc;
using Otus.SocialNetwork.Application.Features.Users.AddFriend;
using Otus.SocialNetwork.Application.Features.Users.GetUserPasswordHash;
using Otus.SocialNetwork.Application.Features.Users.GetUsers;
using Otus.SocialNetwork.Application.Features.Users.RegisterUser;
using Otus.SocialNetwork.Exceptions;
using Otus.SocialNetwork.Infrastructure.Authorization;
using Otus.SocialNetwork.ViewModels;

namespace Otus.SocialNetwork.Controllers;

[Route("users")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly HttpContext _ctx;

    public UsersController(
        IMediator mediator,
        IPasswordHashService passwordHashService,
        IJwtTokenService jwtTokenService,
        HttpContext ctx)
    {
        _mediator = mediator;
        _passwordHashService = passwordHashService;
        _jwtTokenService = jwtTokenService;
        _ctx = ctx;
    }

    [HttpPost("register")]
    public Task RegisterNewUser([FromBody] RegisterUserViewModel viewModel, CancellationToken ct)
    {
        var (hash, salt) = _passwordHashService.CreateHash(viewModel.Password);

        var command = new RegisterUserCommand(
            viewModel.Username,
            viewModel.FirstName,
            viewModel.LastName,
            viewModel.DateOfBirth,
            viewModel.Sex,
            viewModel.Interests,
            viewModel.City,
            hash,
            salt);

        return _mediator.Send(command, ct);
    }

    [HttpPost("login")]
    public async Task<LoginUserResponse> Login([FromBody] LoginUserViewModel viewModel, CancellationToken ct)
    {
        var (userExists, hash, salt) = await _mediator.Send(new GetUserPasswordHash(viewModel.Username), ct);
        if (!userExists || hash is null || salt is null)
        {
            throw new AuthenticationFailedException(viewModel.Username);
        }

        var passwordVerificationResult = _passwordHashService.VerifyPassword(viewModel.Password, hash, salt);
        if (!passwordVerificationResult)
        {
            throw new AuthenticationFailedException(viewModel.Username);
        }

        var token = _jwtTokenService.GenerateToken(viewModel.Username);
        return new(token);
    }

    [HttpGet]
    [CustomAuthorize]
    public Task<GetUsersQueryResult> Get(CancellationToken ct)
    {
        return _mediator.Send(new GetUsersQuery(), ct);
    }

    [HttpPut("friend")]
    [CustomAuthorize]
    public Task Friend([FromBody] FriendRequest request, CancellationToken ct)
    {
        var username = (string?)_ctx.Items[ContextConstants.USERNAME];
        if (username is null)
        {
            throw new InvalidOperationException("Username can't be null");
        }

        return _mediator.Send(new AddFriendCommand(username, request.FriendUsername), ct);
    }
}