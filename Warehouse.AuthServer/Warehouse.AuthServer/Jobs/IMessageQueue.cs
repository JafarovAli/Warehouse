namespace Warehouse.AuthServer.Jobs
{
    public interface IMessageQueue<T>
    {
        Task Enqueue(T message);
    }
}