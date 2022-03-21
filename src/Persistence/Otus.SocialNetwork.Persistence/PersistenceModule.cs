using System.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Otus.SocialNetwork.Persistence.MediatR;
using Otus.SocialNetwork.Persistence.QueryObjects.SaveUser;

namespace Otus.SocialNetwork.Persistence;

public static class PersistenceModule
{
    public static IServiceCollection AddPersistenceModule(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddScoped<IDbConnection>(provider =>
        {
            var connection = provider.GetRequiredService<IUnitOfWorkFactory>().GetDefault().Connection;
            if (connection is null)
            {
                throw new InvalidOperationException("Connection is not initialized.");
            }

            return connection;
        });

        services.Scan(selector =>
        {
            selector
                .FromAssemblyOf<SaveUserQueryObject>()
                .AddClasses(filter => filter.Where(type => type.Name.EndsWith("QueryObject")))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });

        return services;
    }

    public static void AddUnitOfWorkBehavior(this IServiceCollection services)
    {
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(UnitOfWorkPipelineBehavior<,>));
    }
}