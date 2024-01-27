using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IExchangeRateService
    {
        IEnumerable<ExchangeRate> GetExchangeRatesForCountry(string countryCode);
    }
}
