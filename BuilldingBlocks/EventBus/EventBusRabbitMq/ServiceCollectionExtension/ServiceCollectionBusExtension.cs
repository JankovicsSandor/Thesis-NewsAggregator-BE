using EventBusRabbitMQ.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.ServiceCollectionExtension
{
    public static class ServiceCollectionBusExtension
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            string subscriptionClientName = configuration["SubscriptionClientName"];
            string exchangeName = configuration["ExchangeName"] ?? "news_aggregator_bus";
            string exchangeMode = configuration["ExchangeMode"] ?? "direct";
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["RABBITMQ_RETRY"]))
                {
                    retryCount = int.Parse(configuration["RABBITMQ_RETRY"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, services, logger, eventBusSubcriptionsManager, subscriptionClientName, exchangeName, exchangeMode, retryCount);
            });


            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["RABBITMQ_HOST"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["RABBITMQ_USER"]))
                {
                    factory.UserName = configuration["RABBITMQ_USER"];
                }

                if (!string.IsNullOrEmpty(configuration["RABBITMQ_PASS"]))
                {
                    factory.Password = configuration["RABBITMQ_PASS"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["RABBITMQ_RETRY"]))
                {
                    retryCount = int.Parse(configuration["RABBITMQ_RETRY"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });


            return services;
        }
    }
}
