using CurrencyAppApi.Repositories;

namespace CurrencyAppApi.DAL
{
    public interface IUnitOfWork
    {
        IExchangeRateRepository ExchangeRateRepository { get; }
        ISourceRepository SourceRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void CreateTransaction();
        void Commit();
        void Rollback();
    }
}
