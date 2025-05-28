using System;

namespace PMS.Domain.Entities
{
    public class Performance
    {
        public int Id { get; set; }
        public int? PortfolioId { get; set; }
        public int? HoldingId { get; set; }
        public decimal ReturnRate { get; set; }
        public decimal SharpeRatio { get; set; }
        public string Period { get; set; } // e.g., 1M, 3M, 1Y
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Portfolio Portfolio { get; set; }
        public Holding Holding { get; set; }
    }
}