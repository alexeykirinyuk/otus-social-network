using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.SocialNetwork.Kafka.Producers.Abstractions.Posts;
using Otus.SocialNetwork.Kafka.Producers.Posts;

namespace Otus.SocialNetwork.Kafka.Producers;

public static class KafkaProducersModule
{
    public static IServiceCollection AddKafkaProducers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IPostKafkaProducer, PostKafkaProducer>();
        services.Configure<PostKafkaProducerOptions>(configuration.GetSection(nameof(PostKafkaProducerOptions)));

        return services;
    }
}