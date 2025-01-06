using CurrencyAppApi.Models;
using CurrencyAppApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyAppApi.Controllers
{
    [Route("exchangeRates")]
    [ApiController]
    public class ExchangeRateTableController : ControllerBase
    {
        private readonly IExchangeRatesService _exchangeRatesService;

        public ExchangeRateTableController(IExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }

        [HttpGet("latest")]
        public async Task<ActionResult> GetCurrentExchangeRatesAsync([FromQuery]CurrentExchangeRatesDto request)
        {
            var result = await _exchangeRatesService.GetRates(request.TableName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("availableDates")]
        public ActionResult GetAvailableDates(string tableName)
        {
            
            var result =  _exchangeRatesService.GetAvailableDates(tableName);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult> GetExchangeRatesAsync([FromQuery] ExchangeRatesByDateDto request)
        {
            var result = await _exchangeRatesService.GetRatesForDate(request.TableName, request.SelectedDate);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
