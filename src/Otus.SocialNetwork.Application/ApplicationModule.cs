using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Otus.SocialNetwork.Application.Features.Users.RegisterUser;

namespace Otus.SocialNetwork.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(
        this IServiceCollection services)
    {
        services.AddMediatR(typeof(RegisterUserCommand).Assembly);

        return services;
    }
}