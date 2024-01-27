using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExchangeRate>> GetExchangeRates([FromQuery] string countryCode)
        {
            try
            {
                var exchangeRates = _exchangeRateService.GetExchangeRatesForCountry(countryCode);
                if (exchangeRates == null || !exchangeRates.Any())
                {
                    return NotFound($"Exchange rates for country code {countryCode} not found.");
                }

                return Ok(exchangeRates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
