using Otus.SocialNetwork.Application.Features.Users.RegisterUser;

namespace Otus.SocialNetwork;

public static class PresentationModule
{
    public static IServiceCollection AddPresentationModule(
        this IServiceCollection services)
    {
        services.AddScoped<IPasswordHashCalculator, PasswordHashCalculator>();
        return services;
    }
}