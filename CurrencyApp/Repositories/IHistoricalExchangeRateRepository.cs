using CurrencyAppApi.Entities;

namespace CurrencyAppApi.Repositories
{
    public interface IHistoricalExchangeRateRepository
    {
        Task Add(IEnumerable<HistoricalExchangeRate> exchangeRates);
        IEnumerable<HistoricalExchangeRate> Get(string tableName, DateOnly selectedDate);
        DateTime GetLatestPublicationDate(string tableName);
        IReadOnlyCollection<DateOnly> GetPublicationDates(string tableName);
    }
}