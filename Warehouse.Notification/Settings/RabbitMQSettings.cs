namespace Warehouse.Notification.Settings
{
    public class RabbitMQSettings
    {
        public string RabbitMqRootUri { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
    }
}
