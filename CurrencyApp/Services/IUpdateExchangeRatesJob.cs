namespace CurrencyAppApi.Services
{
    public interface IUpdateExchangeRatesJob
    {
        Task UpdateTableData(string table);
    }
}