using System;

namespace PMS.Domain.Entities
{
    public class PriceHistory
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public Asset Asset { get; set; }
    }
}