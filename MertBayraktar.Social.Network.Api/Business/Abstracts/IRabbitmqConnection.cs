using RabbitMQ.Client;

namespace MertBayraktar.Social.Network.Api.Business.Abstracts
{
    
    public interface IRabbitmqConnection
    {
        IConnection GetRabbitMqConnection();
    }
}

