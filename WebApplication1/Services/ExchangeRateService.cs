using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WebApplication1.Models;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly string _jsonFilePath;
        private readonly Dictionary<string, decimal> _flatRates;
        private readonly Dictionary<string, string> _currencyToCountryCode;

        public ExchangeRateService(IConfiguration configuration)
        {
            _jsonFilePath = configuration.GetValue<string>("PartnerRatesJsonPath");

            // Initializing flat rates for countries
            _flatRates = new Dictionary<string, decimal>
            {
                { "MEX", 0.024m },
                { "PHL", 2.437m },
                { "GTM", 0.056m },
                { "IND", 3.213m }
            };

            // Initialize currency to country code mapping
            _currencyToCountryCode = new Dictionary<string, string>
            {
                { "MXN", "MEX" },
                { "PHP", "PHL" },
                { "GTQ", "GTM" },
                { "INR", "IND" }
            };
        }

        public IEnumerable<ExchangeRate> GetExchangeRatesForCountry(string countryCode)
        {
            // Read the JSON file using the provided jsonFilePath
            var jsonData = File.ReadAllText(_jsonFilePath);
            var partnerRatesData = JsonConvert.DeserializeObject<PartnerRatesContainer>(jsonData);

            // Filter and process the rates
            var latestRates = partnerRatesData.PartnerRates
                .Where(rate => _currencyToCountryCode[rate.Currency] == countryCode)
                .GroupBy(rate => new { rate.PaymentMethod, rate.DeliveryMethod })
                .Select(grp => grp.OrderByDescending(rate => rate.AcquiredDate).First())
                .Select(rate => new ExchangeRate
                {
                    CurrencyCode = rate.Currency,
                    CountryCode = _currencyToCountryCode[rate.Currency],
                    PangeaRate = CalculatePangeaRate(rate.Rate, rate.Currency),
                    PaymentMethod = rate.PaymentMethod,
                    DeliveryMethod = rate.DeliveryMethod
                });

            return latestRates;
        }



        public decimal CalculatePangeaRate(decimal partnerRate, string countryCurrencyCode)
        {
            if (_flatRates.TryGetValue(countryCurrencyCode, out var flatRate))
            {
                // Adjust the rate and round up to two decimal places
                return Math.Round(partnerRate + flatRate, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                throw new KeyNotFoundException($"Flat rate not found for country code: {countryCurrencyCode}");
            }
        }
    }
}
