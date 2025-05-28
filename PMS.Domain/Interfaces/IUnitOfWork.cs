using PMS.Domain.Entities;

namespace PMS.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Portfolio> Portfolios { get; }
        IGenericRepository<Client> Clients { get; }
        IGenericRepository<Asset> Assets { get; }
        IGenericRepository<Holding> Holdings { get; }
        IGenericRepository<Transaction> Transactions { get; }
        IGenericRepository<Performance> Performances { get; }
        IGenericRepository<Benchmark> Benchmarks { get; }
        IGenericRepository<Account> Accounts { get; }
        IGenericRepository<Goal> Goals { get; }
        IGenericRepository<Allocation> Allocations { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<Report> Reports { get; }
        IGenericRepository<PriceHistory> PriceHistories { get; }
        Task<int> CompleteAsync();
    }
}