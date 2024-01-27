using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Services;
using Microsoft.Extensions.Configuration;

namespace ExchangeRateTest
{
    [TestClass]
    public class ExchangeRateServiceTests
    {
        [TestMethod]
        public void TestCalculatePangeaRate()
        {
            // Arrange
            var configurations = new Dictionary<string, string>
{
    { "PartnerRatesJsonPath", @"C:\Users\HP\Desktop\Pangea\PangeaCodingTest\WebApplication1\SampleData\partner_rates.json" }
};

            var testConfiguration = new TestConfiguration(configurations);
            var exchangeRateService = new ExchangeRateService(testConfiguration);

            // Act
            var result = exchangeRateService.CalculatePangeaRate(10.0m, "MXN");

            // Assert
            Assert.AreEqual(10.024m, result);
        }
    }
}
