using CurrencyAppApi.Models;

namespace CurrencyAppApi.Services
{
    public interface IExchangeRatesService
    {
        Task<ExchangeTableDto> GetRates(string tableName);
        Task<ExchangeTableDto> GetRatesForDate(string tableName, DateOnly selectedDate);
        IReadOnlyCollection<DateOnly> GetAvailableDates(string tableName);
    }
}