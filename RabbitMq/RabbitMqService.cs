using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel(); 

           
            _channel.QueueDeclare(
                queue: "sms_notifications",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void PublishUserCreatedSms(string sms)
        {
            if (sms == null)
            {
                throw new ArgumentNullException(nameof(sms), "SMS notification cannot be null.");
            }
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sms));
            var props = _channel.CreateBasicProperties();
            props.Persistent = true;

            _channel.BasicPublish("", "sms_notifications", props, body);
        }
    }
}
