using System;

namespace PMS.Application.DTOs
{
    public class PriceHistoryDto
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
    }
}