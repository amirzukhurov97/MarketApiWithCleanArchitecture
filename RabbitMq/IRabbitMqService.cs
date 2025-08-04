using Market.Infrastructure.RebbitMq;

namespace RabbitMq
{
    public interface IRabbitMqService
    {
        void PublishUserCreatedSms(SmsNotification sms);
    }
}
