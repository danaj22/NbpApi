using CurrencyAppApi.DAL;
using CurrencyAppApi.Entities;

namespace CurrencyAppApi.Services
{
    public class ExchangeRatesJob : IExchangeRatesJob
    {
        private readonly ILogger<ExchangeRatesJob> _logger;
        private readonly NbpApiClient _nbpApiClient;
        private readonly IUnitOfWork _unitOfWork;

        public ExchangeRatesJob(ILogger<ExchangeRatesJob> logger,
            NbpApiClient nbpApiClient,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _nbpApiClient = nbpApiClient;
            _unitOfWork = unitOfWork;
        }

        public async Task FetchData()
        {
            _logger.LogInformation("Start FetchData job...");
            foreach (var tableName in NbpConsts.TABLE_NAMES)
            {
                await AddCurrentExchangeRates(tableName);
            }
            _logger.LogInformation("FetchData job completed successfuly.");
        }

        public async Task UpdateData()
        {
            _logger.LogInformation("Start CompleteData job...");
            foreach (var tableType in NbpConsts.TABLE_NAMES)
            {
                try
                {
                    _unitOfWork.CreateTransaction();
                    RemoveOldExchangeRates(tableType);
                    await AddCurrentExchangeRates(tableType);
                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                    _logger.LogError(ex, ex.Message);
                }
            }
            _logger.LogInformation("CompleteData job completed successfuly.");
        }

        private async Task AddCurrentExchangeRates(string tableType)
        {

            _logger.LogInformation("Start fetching data from NBP...");
            var results = await _nbpApiClient.GetExchangeRates(tableType);
            var sourceId = _unitOfWork.SourceRepository.Get(tableType).Id;

            foreach (var result in results)
            {
                _logger.LogInformation($"Fetched {result.Rates.Count()} exchange rates for date: {result.EffectiveDate}.");
                
                var exchangeRates = result.Rates.Select(x => new ExchangeRate()
                {
                    Rate = x.Mid,
                    EffectiveDate = DateTime.Parse(result.EffectiveDate),
                    TargetCurrencyId = x.Code,
                    BaseCurrencyId = NbpConsts.BASE_CURRENCY,
                    SourceId = sourceId
                });

                await _unitOfWork.ExchangeRateRepository.AddRange(exchangeRates);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"All {result.Rates.Count()} exchange rates has been saved to DB.");
            }
        }

        private void RemoveOldExchangeRates(string tableType)
        {
            _logger.LogInformation("Removing old exchange rates...");

            var oldExchangeRates = _unitOfWork.ExchangeRateRepository.Get(tableType);

            _logger.LogInformation($"{oldExchangeRates.Count()} exchange rates has been found in DB.");

            _unitOfWork.ExchangeRateRepository.RemoveRange(oldExchangeRates);
           
            _logger.LogInformation("All old exchange rates has been removed from DB.");
        }
    }
}
