namespace Market.Infrastructure.RebbitMq
{
    public class SmsNotification
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
