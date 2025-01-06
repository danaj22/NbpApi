namespace CurrencyAppApi.Models
{
    public record ExchangeTableDto(DateOnly EffecitveDate, IEnumerable<ExchangeRateDto> ExchangeRates);
}
