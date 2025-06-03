using System;

namespace PMS.Application.DTOs
{
    public class ReportDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}