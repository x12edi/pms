using System;

namespace PMS.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Type { get; set; } // e.g., Performance, Tax
        public string Content { get; set; }
        public DateTime GeneratedAt { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}