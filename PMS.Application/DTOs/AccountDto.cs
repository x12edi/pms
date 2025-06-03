using System;

namespace PMS.Application.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public string Institution { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}