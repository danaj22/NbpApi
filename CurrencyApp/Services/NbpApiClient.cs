using CurrencyAppApi.Models;

namespace CurrencyAppApi.Services
{
    public class NbpApiClient(HttpClient httpClient, ILogger<ApiClient> logger) : ApiClient(httpClient, logger)
    {
        public async Task<IEnumerable<NbpExchangeRatesDto>> GetExchangeRates(string table, CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<IEnumerable<NbpExchangeRatesDto>>($"exchangerates/tables/{table}", cancellationToken);
            
            if (result == null)
                return [];

            return result;
        }

        public async Task<IEnumerable<NbpExchangeRatesDto>> GetExchangeRatesBetweenDates(string table, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<IEnumerable<NbpExchangeRatesDto>>($"exchangerates/tables/{table}/{startDate.ToString("yyyy-MM-dd")}/{endDate.ToString("yyyy-MM-dd")}", cancellationToken);

            if (result == null)
                return [];

            return result;
        }

        public async Task<NbpExchangeRatesDto> GetCurrencyRate(string table, string currencyCode, int numberOfLastRates = 1, CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<NbpExchangeRatesDto>($"exchangerates/tables/{table}/{currencyCode}/last/{numberOfLastRates}", cancellationToken);

            return result;
        }

        public async Task<NbpExchangeRatesDto> GetCurrencyRateBetweenDates(string table, string currencyCode, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<NbpExchangeRatesDto>($"exchangerates/tables/{table}/{currencyCode}/{startDate}/{endDate}", cancellationToken);

            return result;
        }
    }
}
