namespace WebApplication1.Models
{
    public class ExchangeRate
    {
        public string CurrencyCode { get; set; }
        public string CountryCode { get; set; }
        public decimal PangeaRate { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryMethod { get; set; }
    }
}
