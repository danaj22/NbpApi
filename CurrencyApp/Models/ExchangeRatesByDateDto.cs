namespace CurrencyAppApi.Models
{
    public record ExchangeRatesByDateDto(string TableName, DateOnly SelectedDate);
}
