using System;

namespace PMS.Application.DTOs
{
    public class GoalDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public DateTime TargetDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}