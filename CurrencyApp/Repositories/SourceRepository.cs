using CurrencyAppApi.Entities;
using CurrencyAppApi.Services;

namespace CurrencyAppApi.Repositories
{
    public class SourceRepository : ISourceRepository
    {
        private readonly CurrencyDbContext _dbContext;
        public SourceRepository(CurrencyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Source Get(string tableName) 
        {
            var result = _dbContext.Sources
                .Where(x => x.Type == TableNameHelper.GetTableType(tableName))
                .First();

            return result;
        }
    }
}
