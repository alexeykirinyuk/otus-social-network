using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.SocialNetwork.Kafka.Consumers.Posts;

namespace Otus.SocialNetwork.Kafka.Consumers;

public static class KafkaConsumersModule
{
    public static IServiceCollection AddKafkaConsumers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<PostKafkaConsumer>();
        services.Configure<PostKafkaConsumerOptions>(configuration.GetSection(nameof(PostKafkaConsumerOptions)));
        return services;
    }
}