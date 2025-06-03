using PMS.Domain.Entities;
using System;
using System.Linq;

namespace PMS.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(PmsDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if data exists
            if (context.Clients.Any())
                return;

            // Seed Clients
            var client = new Client
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123-456-7890",
                Address = "123 Main St",
                RiskTolerance = "Moderate",
                CreatedAt = DateTime.UtcNow
            };
            context.Clients.Add(client);
            context.SaveChanges();

            // Seed Portfolios
            var portfolio1 = new Portfolio
            {
                Name = "Growth Portfolio",
                ClientId = client.Id,
                Description = "High-growth investments",
                TotalValue = 100000,
                RiskProfile = "Aggressive",
                CreatedAt = DateTime.UtcNow
            };
            var portfolio2 = new Portfolio
            {
                Name = "Retirement Portfolio",
                ClientId = client.Id,
                Description = "Stable investments",
                TotalValue = 50000,
                RiskProfile = "Conservative",
                CreatedAt = DateTime.UtcNow
            };
            context.Portfolios.AddRange(portfolio1, portfolio2);
            context.SaveChanges();

            // Seed Assets
            var asset1 = new Asset
            {
                Name = "Apple Inc.",
                Ticker = "AAPL",
                AssetType = "Stock",
                CurrentPrice = 180.50m,
                Currency = "USD",
                CreatedAt = DateTime.UtcNow
            };
            var asset2 = new Asset
            {
                Name = "S&P 500 ETF",
                Ticker = "SPY",
                AssetType = "ETF",
                CurrentPrice = 420.75m,
                Currency = "USD",
                CreatedAt = DateTime.UtcNow
            };
            context.Assets.AddRange(asset1, asset2);
            context.SaveChanges();

            // Seed Holdings
            var holding1 = new Holding
            {
                PortfolioId = portfolio1.Id,
                AssetId = asset1.Id,
                Quantity = 100,
                PurchasePrice = 175.00m,
                PurchaseDate = DateTime.UtcNow.AddMonths(-6),
                CurrentValue = 18050.00m
            };
            var holding2 = new Holding
            {
                PortfolioId = portfolio1.Id,
                AssetId = asset2.Id,
                Quantity = 50,
                PurchasePrice = 400.00m,
                PurchaseDate = DateTime.UtcNow.AddMonths(-3),
                CurrentValue = 21037.50m
            };
            var holding3 = new Holding
            {
                PortfolioId = portfolio2.Id,
                AssetId = asset2.Id,
                Quantity = 30,
                PurchasePrice = 410.00m,
                PurchaseDate = DateTime.UtcNow.AddMonths(-4),
                CurrentValue = 12622.50m
            };
            context.Holdings.AddRange(holding1, holding2, holding3);
            context.SaveChanges();

            // Seed Transactions
            var transaction1 = new Transaction
            {
                HoldingId = holding1.Id,
                Type = "Buy",
                Quantity = 100,
                Price = 175.00m,
                Amount = 17500.00m,
                Date = DateTime.UtcNow.AddMonths(-6),
                Fees = 10.00m
            };
            var transaction2 = new Transaction
            {
                HoldingId = holding2.Id,
                Type = "Buy",
                Quantity = 50,
                Price = 400.00m,
                Amount = 20000.00m,
                Date = DateTime.UtcNow.AddMonths(-3),
                Fees = 15.00m
            };
            var transaction3 = new Transaction
            {
                HoldingId = holding3.Id,
                Type = "Buy",
                Quantity = 30,
                Price = 410.00m,
                Amount = 12300.00m,
                Date = DateTime.UtcNow.AddMonths(-4),
                Fees = 8.00m
            };
            var transaction4 = new Transaction
            {
                HoldingId = holding1.Id,
                Type = "Sell",
                Quantity = 10,
                Price = 180.00m,
                Amount = 1800.00m,
                Date = DateTime.UtcNow.AddDays(-10),
                Fees = 5.00m
            };
            context.Transactions.AddRange(transaction1, transaction2, transaction3, transaction4);
            context.SaveChanges();
        }
    }
}
/*
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PMS.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(PmsDbContext context)
        {
            if (context.Clients.Any()) return;

            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "123-456-7890",
                    Address = "123 Main St",
                    RiskTolerance = "Medium",
                    CreatedAt = DateTime.UtcNow,
                    Portfolios = new List<Portfolio>
                    {
                        new Portfolio
                        {
                            Name = "Retirement Fund",
                            Description = "Long-term growth",
                            TotalValue = 100000,
                            RiskProfile = "Moderate",
                            CreatedAt = DateTime.UtcNow,
                            Holdings = new List<Holding>
                            {
                                new Holding
                                {
                                    Asset = new Asset
                                    {
                                        Name = "Apple Inc.",
                                        Ticker = "AAPL",
                                        AssetType = "Stock",
                                        CurrentPrice = 150,
                                        Currency = "USD",
                                        CreatedAt = DateTime.UtcNow
                                    },
                                    Quantity = 100,
                                    PurchasePrice = 140,
                                    PurchaseDate = DateTime.UtcNow.AddMonths(-6),
                                    CurrentValue = 15000,
                                    Transactions = new List<Transaction>
                                    {
                                        new Transaction
                                        {
                                            Type = "Buy",
                                            Quantity = 100,
                                            Price = 140,
                                            Amount = 14000,
                                            Date = DateTime.UtcNow.AddMonths(-6),
                                            Fees = 10
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();
        }
    }
} */