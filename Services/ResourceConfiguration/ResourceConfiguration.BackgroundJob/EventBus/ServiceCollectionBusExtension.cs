using Autofac;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceConfiguration.BackgroundJob.EventBus
{
    public static class ServiceCollectionBusExtension
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["RABBITMQ_RETRY"]))
                {
                    retryCount = int.Parse(configuration["RABBITMQ_RETRY"]);
                }

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, services, logger, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
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
