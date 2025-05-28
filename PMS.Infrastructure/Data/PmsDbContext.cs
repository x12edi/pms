using Microsoft.EntityFrameworkCore;
using PMS.Domain.Entities;

namespace PMS.Infrastructure.Data
{
    public class PmsDbContext : DbContext
    {
        public PmsDbContext(DbContextOptions<PmsDbContext> options) : base(options)
        {
        }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Holding> Holdings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Benchmark> Benchmarks { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<PriceHistory> PriceHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Portfolio-Client relationship
            modelBuilder.Entity<Portfolio>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Portfolios)
                .HasForeignKey(p => p.ClientId);

            // Holding-Portfolio and Holding-Asset relationships
            modelBuilder.Entity<Holding>()
                .HasOne(h => h.Portfolio)
                .WithMany(p => p.Holdings)
                .HasForeignKey(h => h.PortfolioId);
            modelBuilder.Entity<Holding>()
                .HasOne(h => h.Asset)
                .WithMany(a => a.Holdings)
                .HasForeignKey(h => h.AssetId);

            // Transaction-Holding relationship
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Holding)
                .WithMany(h => h.Transactions)
                .HasForeignKey(t => t.HoldingId);

            // Performance-Portfolio and Performance-Holding relationships
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Portfolio)
                .WithMany()
                .HasForeignKey(p => p.PortfolioId)
                .IsRequired(false);
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Holding)
                .WithMany()
                .HasForeignKey(p => p.HoldingId)
                .IsRequired(false);

            // Account-Client relationship
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.ClientId);

            // Goal-Portfolio relationship
            modelBuilder.Entity<Goal>()
                .HasOne(g => g.Portfolio)
                .WithMany(p => p.Goals)
                .HasForeignKey(g => g.PortfolioId);

            // Allocation-Portfolio relationship
            modelBuilder.Entity<Allocation>()
                .HasOne(a => a.Portfolio)
                .WithMany(p => p.Allocations)
                .HasForeignKey(a => a.PortfolioId);

            // Report-Portfolio relationship
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Portfolio)
                .WithMany()
                .HasForeignKey(r => r.PortfolioId);

            // PriceHistory-Asset relationship
            modelBuilder.Entity<PriceHistory>()
                .HasOne(ph => ph.Asset)
                .WithMany(a => a.PriceHistories)
                .HasForeignKey(ph => ph.AssetId);
        }
    }
}