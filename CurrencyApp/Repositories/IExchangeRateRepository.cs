using CurrencyAppApi.Entities;

namespace CurrencyAppApi.Repositories
{
    public interface IExchangeRateRepository
    {
        IEnumerable<ExchangeRate> Get(string tableName);
        void RemoveRange(IEnumerable<ExchangeRate> exchangeRates);
        Task AddRange(IEnumerable<ExchangeRate> exchangeRates);
    }
}