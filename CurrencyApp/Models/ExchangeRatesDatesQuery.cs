using Microsoft.AspNetCore.Mvc;

namespace CurrencyAppApi.Models
{
    public record ExchangeRatesDatesQuery
    {
        [FromRoute(Name = "startDate")]
        public DateOnly StartDate { get; init; }

        [FromRoute(Name = "endDate")]
        public DateOnly EndDate { get; init; }
    }
}
