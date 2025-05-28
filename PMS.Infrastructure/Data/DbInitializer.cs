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
}