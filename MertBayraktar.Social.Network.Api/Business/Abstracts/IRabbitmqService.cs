using MertBayraktar.Social.Network.Api.Entities.Data;

namespace MertBayraktar.Social.Network.Api.Business.Abstracts
{
    public interface IRabbitmqService
    {
        void Publish(User user, string exchangeType, string exchangeName, string queueName, string routeKey);
    }
}
