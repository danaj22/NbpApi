using CurrencyAppApi.Entities;
using CurrencyAppApi.Models;
using CurrencyAppApi.Repositories;

namespace CurrencyAppApi.Services
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly IHistoricalExchangeRateRepository _historicalExchangeRateRepository;
        private readonly ICacheService _cacheService;

        public ExchangeRatesService(IExchangeRateRepository exchangeRateRepository, IHistoricalExchangeRateRepository historicalExchangeRateRepository, ICacheService cacheService) 
        {
            _exchangeRateRepository = exchangeRateRepository;
            _historicalExchangeRateRepository = historicalExchangeRateRepository;
            _cacheService = cacheService;
        }

        public async Task<ExchangeTableDto> GetRates(string tableName)
        {
            var cacheKey = $"Rates:{tableName}_{DateOnly.FromDateTime(DateTime.UtcNow)}";
            var cachedRates = await _cacheService.GetCacheValueAsync<ExchangeTableDto>(cacheKey);

            if(cachedRates != null)
            {
                return cachedRates;
            }

            var exchangeRates = _exchangeRateRepository.Get(tableName);

            var rates = exchangeRates.Select(x => new ExchangeRateDto(x.TargetCurrencyId, x.TargetCurrency.Name, x.Rate));
            var efectiveDate = DateOnly.FromDateTime(exchangeRates.FirstOrDefault().EffectiveDate);

            var result = new ExchangeTableDto(efectiveDate, rates);

            if (result != null)
            {
                await _cacheService.SetCacheValueAsync(cacheKey, result, TimeSpan.FromMinutes(5));
            }

            return result;
        }

        public async Task<ExchangeTableDto> GetRatesForDate(string tableName, DateOnly selectedDate)
        {
            var cacheKey = $"Rates:{tableName}_{selectedDate}";
            var cachedRates = await _cacheService.GetCacheValueAsync<ExchangeTableDto>(cacheKey);

            if (cachedRates != null)
            {
                return cachedRates;
            }

            var exchangeRates = _historicalExchangeRateRepository.Get(tableName, selectedDate);

            var rates = exchangeRates.Select(x => new ExchangeRateDto(x.TargetCurrencyId, x.TargetCurrency.Name, x.Rate));
            
            var result = new ExchangeTableDto(selectedDate, rates);

            if (result != null)
            {
                await _cacheService.SetCacheValueAsync(cacheKey, result, TimeSpan.FromMinutes(5));
            }

            return result;
        }

        public IReadOnlyCollection<DateOnly> GetAvailableDates(string tableName)
        {
            var result = _historicalExchangeRateRepository.GetPublicationDates(tableName);
            
            return result;
        }
    }

    public class TableNameHelper
    {
        public static TableType GetTableType(string tableName)
        {
            var _tableDictionary = new Dictionary<string, TableType>() 
            {
                { "A", TableType.TableA },
                { "B", TableType.TableB }
            };

            return _tableDictionary.ContainsKey(tableName.ToUpper()) ? _tableDictionary[tableName.ToUpper()] : throw new Exception($"Table name {tableName} does not exist.");
        }
    }
}
