using System;

namespace PMS.Domain.Entities
{
    public class Benchmark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal ReturnRate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}