using Microsoft.EntityFrameworkCore;

namespace CurrencyAppApi.Entities
{
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options)
        {
            
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<HistoricalExchangeRate> HistoricalExchangeRates { get; set; }
        public DbSet<Source> Sources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Currency>()
                .Property(x => x.Id)
                .HasMaxLength(3);

            modelBuilder.Entity<Currency>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Currency>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Currency>()
                .Property(x => x.UpdatedAt)
                .ValueGeneratedOnUpdate();

            modelBuilder.Entity<ExchangeRate>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ExchangeRate>()
                .Property(x => x.Rate)
                .IsRequired();

            modelBuilder.Entity<ExchangeRate>()
                .Property(x => x.TargetCurrencyId)
                .IsRequired();

            modelBuilder.Entity<ExchangeRate>()
                .Property(x => x.BaseCurrencyId)
                .IsRequired();

            modelBuilder.Entity<ExchangeRate>()
                .Property(x => x.EffectiveDate)
                .IsRequired();

            modelBuilder.Entity<ExchangeRate>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(x => x.Source)
                .WithMany(x => x.ExchangeRates)
                .HasForeignKey(x => x.SourceId);

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(x => x.BaseCurrency)
                .WithMany(x => x.ExchangeRates)
                .HasForeignKey(x => x.BaseCurrencyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(x => x.TargetCurrency)
                .WithMany()
                .HasForeignKey(x => x.TargetCurrencyId)
                .OnDelete(DeleteBehavior.NoAction);
                

            modelBuilder.Entity<HistoricalExchangeRate>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<HistoricalExchangeRate>()
                .Property(x => x.Rate)
                .IsRequired();

            modelBuilder.Entity<HistoricalExchangeRate>()
                .Property(x => x.TargetCurrencyId)
                .IsRequired();
            
            modelBuilder.Entity<HistoricalExchangeRate>()
                .Property(x => x.BaseCurrencyId)
                .IsRequired();

            modelBuilder.Entity<HistoricalExchangeRate>()
                .Property(x => x.EffectiveDate)
                .IsRequired();

            modelBuilder.Entity<HistoricalExchangeRate>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<HistoricalExchangeRate>()
                .HasOne(x => x.Source)
                .WithMany(x => x.HistoricalExchangeRates)
                .HasForeignKey(x => x.SourceId); 

            modelBuilder.Entity<HistoricalExchangeRate>()
                .HasOne(x => x.BaseCurrency)
                .WithMany(x => x.HistoricalExchangeRates)
                .HasForeignKey(x => x.BaseCurrencyId)
                .OnDelete(DeleteBehavior.NoAction); ; 
            
            modelBuilder.Entity<HistoricalExchangeRate>()
                .HasOne(x => x.TargetCurrency)
                .WithMany()
                .HasForeignKey(x => x.TargetCurrencyId)
                .OnDelete(DeleteBehavior.NoAction); ;

            modelBuilder.Entity<Source>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Source>()
                .Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Source>()
                .Property(x => x.Type)
                .IsRequired(false);

            modelBuilder.Entity<Source>()
                .Property(x=> x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
