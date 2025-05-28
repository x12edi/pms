using PMS.Domain.Entities;
using PMS.Domain.Interfaces;
using PMS.Infrastructure.Data;
using System.Threading.Tasks;

namespace PMS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PmsDbContext _context;
        private IGenericRepository<Portfolio> _portfolios;
        private IGenericRepository<Client> _clients;
        private IGenericRepository<Asset> _assets;
        private IGenericRepository<Holding> _holdings;
        private IGenericRepository<Transaction> _transactions;
        private IGenericRepository<Performance> _performances;
        private IGenericRepository<Benchmark> _benchmarks;
        private IGenericRepository<Account> _accounts;
        private IGenericRepository<Goal> _goals;
        private IGenericRepository<Allocation> _allocations;
        private IGenericRepository<User> _users;
        private IGenericRepository<Report> _reports;
        private IGenericRepository<PriceHistory> _priceHistories;

        public UnitOfWork(PmsDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Portfolio> Portfolios => _portfolios ??= new GenericRepository<Portfolio>(_context);
        public IGenericRepository<Client> Clients => _clients ??= new GenericRepository<Client>(_context);
        public IGenericRepository<Asset> Assets => _assets ??= new GenericRepository<Asset>(_context);
        public IGenericRepository<Holding> Holdings => _holdings ??= new GenericRepository<Holding>(_context);
        public IGenericRepository<Transaction> Transactions => _transactions ??= new GenericRepository<Transaction>(_context);
        public IGenericRepository<Performance> Performances => _performances ??= new GenericRepository<Performance>(_context);
        public IGenericRepository<Benchmark> Benchmarks => _benchmarks ??= new GenericRepository<Benchmark>(_context);
        public IGenericRepository<Account> Accounts => _accounts ??= new GenericRepository<Account>(_context);
        public IGenericRepository<Goal> Goals => _goals ??= new GenericRepository<Goal>(_context);
        public IGenericRepository<Allocation> Allocations => _allocations ??= new GenericRepository<Allocation>(_context);
        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
        public IGenericRepository<Report> Reports => _reports ??= new GenericRepository<Report>(_context);
        public IGenericRepository<PriceHistory> PriceHistories => _priceHistories ??= new GenericRepository<PriceHistory>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}