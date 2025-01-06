using CurrencyAppApi.Entities;

namespace CurrencyAppApi.Repositories
{
    public interface ISourceRepository
    {
        Source Get(string tableName);
    }
}