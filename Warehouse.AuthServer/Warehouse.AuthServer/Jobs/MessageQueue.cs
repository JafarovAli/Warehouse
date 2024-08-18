using MassTransit;

namespace Warehouse.AuthServer.Jobs
{
    public class MessageQueue<T> : IMessageQueue<T>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public MessageQueue(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Enqueue(T message)
        {
            await publishEndpoint.Publish(message);
        }
    }
}
