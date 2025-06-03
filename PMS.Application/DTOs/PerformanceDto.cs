using System;

namespace PMS.Application.DTOs
{
    public class PerformanceDto
    {
        public int Id { get; set; }
        public int? PortfolioId { get; set; }
        public int? HoldingId { get; set; }
        public decimal ReturnRate { get; set; }
        public decimal SharpeRatio { get; set; }
        public string Period { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}