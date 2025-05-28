using System;
using System.Collections.Generic;

namespace PMS.Domain.Entities
{
    public class Holding
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int AssetId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CurrentValue { get; set; }
        public Portfolio Portfolio { get; set; }
        public Asset Asset { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
    }
}