using System;

namespace PMS.Application.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int HoldingId { get; set; }
        public string Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal Fees { get; set; }
        public string? PortfolioName { get; set; }
        public string? HoldingTicker { get; set; }
    }
}