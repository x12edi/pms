using System;

namespace PMS.Application.DTOs
{
    public class HoldingDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; } // Flattened from Asset
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal CurrentValue { get; set; }
    }
}