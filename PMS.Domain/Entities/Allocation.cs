using System;

namespace PMS.Domain.Entities
{
    public class Allocation
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string AssetType { get; set; } // e.g., Equity, FixedIncome
        public decimal TargetPercentage { get; set; }
        public decimal ActualPercentage { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}