using MediatR;
using Otus.SocialNetwork.Application.Features.Users.RegisterUser;
using Otus.SocialNetwork.Infrastructure.Authorization;
using Otus.SocialNetwork.Persistence;

namespace Otus.SocialNetwork;

public static class PresentationModule
{
    public static IServiceCollection AddPresentationModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<IJwtToketService, JwtTokenService>();
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));

        services.AddUnitOfWorkBehavior();

        return services;
    }
}