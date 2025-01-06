namespace CurrencyAppApi.Services
{
    public interface IHistoricalExchangeRatesJob
    {
        Task CompleteData();
        Task FetchData();
    }
}