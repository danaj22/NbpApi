
using CurrencyAppApi.Entities;
using CurrencyAppApi.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CurrencyAppApi.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CurrencyDbContext _dbContext;
        private IDbContextTransaction? transactionObject = null;
        private IExchangeRateRepository? exchangeRateRepository = null;
        private IHistoricalExchangeRateRepository? historicalExchangeRateRepository = null;
        private ISourceRepository? sourceRepository = null;
        
        public IExchangeRateRepository ExchangeRateRepository
        {
            get
            {
                if(exchangeRateRepository == null )
                    exchangeRateRepository = new ExchangeRateRepository(_dbContext);
                return exchangeRateRepository;
            }
        }

        public ISourceRepository SourceRepository
        {
            get
            {
                if (sourceRepository == null)
                    sourceRepository = new SourceRepository(_dbContext);
                return sourceRepository;
            }
        }

        public IHistoricalExchangeRateRepository? HistoricalExchangeRateRepository
        { 
            get
            {
                if (historicalExchangeRateRepository == null)
                    historicalExchangeRateRepository = new HistoricalExchangeRateRepository(_dbContext);
                return historicalExchangeRateRepository;
            }
        }

        public UnitOfWork(CurrencyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _dbContext.Dispose();
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void CreateTransaction()
        {
            transactionObject = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            transactionObject.Commit();
        }

        public void Rollback()
        {
            transactionObject.Rollback();
            transactionObject.Dispose();
        }
    }
}
