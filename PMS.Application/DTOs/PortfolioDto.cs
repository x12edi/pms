using System;

namespace PMS.Application.DTOs
{
    public class PortfolioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } // Flattened from Client
        public string Description { get; set; }
        public decimal TotalValue { get; set; }
        public string RiskProfile { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}