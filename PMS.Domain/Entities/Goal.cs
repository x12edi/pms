using System;

namespace PMS.Domain.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public DateTime TargetDate { get; set; }
        public string Priority { get; set; } // e.g., High, Medium, Low
        public string Status { get; set; } // e.g., Active, Achieved
        public Portfolio Portfolio { get; set; }
    }
}