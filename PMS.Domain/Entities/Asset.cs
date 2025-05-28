using System;
using System.Collections.Generic;

namespace PMS.Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
        public string AssetType { get; set; } // e.g., Stock, MutualFund, Bond, ETF, RealEstate
        public decimal CurrentPrice { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Holding> Holdings { get; set; } = new();
        public List<PriceHistory> PriceHistories { get; set; } = new();
    }
}