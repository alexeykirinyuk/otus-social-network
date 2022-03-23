namespace Otus.SocialNetwork.Infrastructure.Authorization;

public sealed class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtTokenService jwtTokenService)
    {
        var token = context.Request
            .Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ")
            .LastOrDefault();

        var username = await jwtTokenService.GetUsername(token);

        context.Items[ContextConstants.USERNAME] = username;
        context.Items[ContextConstants.IS_AUTHENTICATED] = username is not null;

        await _next(context);
    }
}