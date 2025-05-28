using System;

namespace PMS.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // e.g., Admin, Advisor
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}