
namespace RabbitMq
{
    public interface IRabbitMqService
    {
        void PublishUserCreatedSms(string sms);
    }
}
