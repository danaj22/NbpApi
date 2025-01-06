using CurrencyAppApi.Entities;
using CurrencyAppApi.Repositories;

namespace CurrencyAppApi.Services
{
    public class HistoricalExchangeRatesJob : IHistoricalExchangeRatesJob
    {
        private readonly ILogger<ExchangeRatesJob> _logger;
        private readonly ISourceRepository _sourceRepository;
        private readonly NbpApiClient _nbpApiClient;
        private readonly IHistoricalExchangeRateRepository _historicalExchangeRateRepository;

        public HistoricalExchangeRatesJob(NbpApiClient nbpApiClient, 
            ILogger<ExchangeRatesJob> logger, 
            ISourceRepository sourceRepository, 
            IHistoricalExchangeRateRepository historicalExchangeRateRepository)
        {
            _logger = logger;
            _sourceRepository = sourceRepository;
            _nbpApiClient = nbpApiClient;
            _historicalExchangeRateRepository = historicalExchangeRateRepository;
        }
        public async Task FetchData()
        {
            _logger.LogInformation("Start FetchData job...");

            foreach (var tableType in NbpConsts.TABLE_NAMES)
            {
                await AddHistoricalExchangeRates(tableType, DateTime.Today.AddDays(-NbpConsts.NBP_MAX_DAYS_BACK));
            }

            _logger.LogInformation("FetchData job completed successfuly.");
        }

        public async Task CompleteData()
        {
            _logger.LogInformation("Start CompleteData job...");

            foreach (var tableType in NbpConsts.TABLE_NAMES)
            {
                await JoinHistoricalExchangeRates(tableType);
            }

            _logger.LogInformation("CompleteData job completed successfuly.");
        }

        private async Task AddHistoricalExchangeRates(string tableType, DateTime dateFrom)
        {
            _logger.LogInformation("Start fetching data from NBP...");
            var results = await _nbpApiClient.GetExchangeRatesBetweenDates(tableType, dateFrom, DateTime.Today);
            var sourceId = _sourceRepository.Get(tableType).Id;

            foreach (var result in results)
            {
                _logger.LogInformation($"Fetched {result.Rates.Count()} historical exchange rates for date: {result.EffectiveDate}.");
                var historicalExchangeRates = result.Rates.Select(x => new HistoricalExchangeRate()
                {
                    Rate = x.Mid,
                    EffectiveDate = DateTime.Parse(result.EffectiveDate),
                    TargetCurrencyId = x.Code,
                    BaseCurrencyId = NbpConsts.BASE_CURRENCY,
                    SourceId = sourceId
                });

                await _historicalExchangeRateRepository.Add(historicalExchangeRates);

                _logger.LogInformation($"All {result.Rates.Count()} historical exchange rates has been saved to DB.");
            }
        }

        private async Task JoinHistoricalExchangeRates(string tableName)
        {
            var latestExchangeRateDate = _historicalExchangeRateRepository.GetLatestPublicationDate(tableName);

            var startDate = latestExchangeRateDate;

            if(DateTime.Now.Date > latestExchangeRateDate)
            {
                startDate = latestExchangeRateDate.AddDays(1);
            }


            await AddHistoricalExchangeRates(tableName, startDate);
        }
    }
}
