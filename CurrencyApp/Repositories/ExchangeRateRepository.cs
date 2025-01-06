using CurrencyAppApi.Models;
using CurrencyAppApi.Services;
using Microsoft.EntityFrameworkCore;
using CurrencyAppApi.Entities;
using CurrencyAppApi.DAL;

namespace CurrencyAppApi.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly CurrencyDbContext _dbContext;

        public ExchangeRateRepository(CurrencyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRange(IEnumerable<ExchangeRate> exchangeRates)
        {
            await _dbContext.ExchangeRates.AddRangeAsync(exchangeRates);
        }

        public IEnumerable<ExchangeRate> Get(string tableName)
        {
            var result = _dbContext.ExchangeRates
                .AsNoTracking()
                .Include(x=> x.TargetCurrency)
                .Where(x => x.Source.Type == TableNameHelper.GetTableType(tableName))
                .ToList();

            return result;
        }

        public void RemoveRange(IEnumerable<ExchangeRate> exchangeRates)
        {
            _dbContext.ExchangeRates.RemoveRange(exchangeRates);
        }
    }
}
