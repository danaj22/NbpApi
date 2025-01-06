using System.ComponentModel.DataAnnotations;

namespace CurrencyAppApi.Entities
{
    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TableType? Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<ExchangeRate> ExchangeRates { get; set; }
        public ICollection<HistoricalExchangeRate> HistoricalExchangeRates { get; set; }
    }

    public enum TableType
    {
        TableA = 1,
        TableB
    }
}
