using CurrencyAppApi.Entities;
using CurrencyAppApi.Services;
using Microsoft.EntityFrameworkCore;

namespace CurrencyAppApi.Repositories
{
    public class HistoricalExchangeRateRepository : IHistoricalExchangeRateRepository
    {
        private readonly CurrencyDbContext _dbContext;
        public HistoricalExchangeRateRepository(CurrencyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<HistoricalExchangeRate> Get(string tableName, DateOnly selectedDate)
        {
            var result = _dbContext.HistoricalExchangeRates
                .AsNoTracking()
                .Include(x=> x.TargetCurrency)
                .Where(x => x.Source.Type == TableNameHelper.GetTableType(tableName) 
                        && DateOnly.FromDateTime(x.EffectiveDate) == selectedDate)
                .ToList();

            return result;
        }

        public IReadOnlyCollection<DateOnly> GetPublicationDates(string tableName)
        {
            var result = _dbContext.HistoricalExchangeRates
                .AsNoTracking()
                .Where(x => x.Source.Type == TableNameHelper.GetTableType(tableName))
                .Select(x => DateOnly.FromDateTime(x.EffectiveDate))
                .Distinct()
                .ToList();

            return result;
        }

        public async Task Add(IEnumerable<HistoricalExchangeRate> exchangeRates)
        {
            await _dbContext.HistoricalExchangeRates.AddRangeAsync(exchangeRates);
            await _dbContext.SaveChangesAsync();
        }

        public DateTime GetLatestPublicationDate(string tableName)
        {
            var result = _dbContext.HistoricalExchangeRates
                            .Where(x => x.Source.Type == TableNameHelper.GetTableType(tableName))
                            .Max(x => x.EffectiveDate);

            return result;
        }
    }
}
