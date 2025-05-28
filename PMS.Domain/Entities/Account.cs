using System;
using System.Collections.Generic;

namespace PMS.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string AccountNumber { get; set; }
        public string Type { get; set; } // e.g., Individual, Retirement
        public decimal Balance { get; set; }
        public string Institution { get; set; }
        public DateTime CreatedAt { get; set; }
        public Client Client { get; set; }
    }
}