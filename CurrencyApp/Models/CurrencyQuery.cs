using Microsoft.AspNetCore.Mvc;

namespace CurrencyAppApi.Models
{
    public record CurrencyQuery
    {
        public string Code { get; init; }
        public string TableName { get; init; }
    }
}
