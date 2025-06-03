using System;

namespace PMS.Application.DTOs
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
        public string AssetType { get; set; }
        public decimal CurrentPrice { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}