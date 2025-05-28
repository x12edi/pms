using System;
using System.Collections.Generic;

namespace PMS.Domain.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public decimal TotalValue { get; set; }
        public string RiskProfile { get; set; } // e.g., Conservative, Aggressive
        public DateTime CreatedAt { get; set; }
        public Client Client { get; set; }
        public List<Holding> Holdings { get; set; } = new();
        public List<Goal> Goals { get; set; } = new();
        public List<Allocation> Allocations { get; set; } = new();
    }
}