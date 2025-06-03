using System;

namespace PMS.Application.DTOs
{
    public class AllocationDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string AssetType { get; set; }
        public decimal TargetPercentage { get; set; }
        public decimal ActualPercentage { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}