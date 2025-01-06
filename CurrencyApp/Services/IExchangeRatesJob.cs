namespace CurrencyAppApi.Services
{
    public interface IExchangeRatesJob
    {
        Task UpdateData();
        Task FetchData();
    }
}