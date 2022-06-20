using MertBayraktar.Social.Network.Api.Business.Abstracts;
using RabbitMQ.Client;

namespace MertBayraktar.Social.Network.Api.Business.Concretes
{
    public class RabbitmqConnection : IRabbitmqConnection
    {
        public IConnection GetRabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "/",
                Port = 5672,
                UserName = "guest",
                Password = "guest"

            }.CreateConnection();

            return connectionFactory;
        }
    }
}
