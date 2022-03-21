using MediatR;
using Otus.SocialNetwork.Application.Features.Users.RegisterUser;
using Otus.SocialNetwork.Persistence;

namespace Otus.SocialNetwork;

public static class PresentationModule
{
    public static IServiceCollection AddPresentationModule(
        this IServiceCollection services)
    {
        services.AddScoped<IPasswordHashCalculator, PasswordHashCalculator>();
        
        services.AddUnitOfWorkBehavior();
        
        return services;
    }
}