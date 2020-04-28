using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection
    {
        bool IsConnected { get; }

        IModel CreateModel();
        void Dispose();
        bool TryConnect();
    }
}