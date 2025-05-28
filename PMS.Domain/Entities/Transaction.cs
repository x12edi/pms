using System;

namespace PMS.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int HoldingId { get; set; }
        public string Type { get; set; } // e.g., Buy, Sell, Dividend
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal Fees { get; set; }
        public Holding Holding { get; set; }
    }
}