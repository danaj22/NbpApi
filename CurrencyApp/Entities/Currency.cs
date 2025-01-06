using System.ComponentModel.DataAnnotations;

namespace CurrencyAppApi.Entities
{
    public class Currency
    {
        public required string Id { get; set; } //ISO 4217
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }

        public ICollection<ExchangeRate>? ExchangeRates { get; set; }
        public ICollection<HistoricalExchangeRate>? HistoricalExchangeRates { get; set; }
    }
}
