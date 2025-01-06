namespace CurrencyAppApi.Entities
{
    public class ExchangeRate
    {
        public Guid Id { get; set; }
        public required string BaseCurrencyId { get; set; }
        public required string TargetCurrencyId { get; set; }
        public double Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SourceId { get; set; }

        public virtual Currency? BaseCurrency { get; set; }
        public virtual Currency? TargetCurrency { get; set; }
        public virtual Source? Source { get; set; }

    }
}
