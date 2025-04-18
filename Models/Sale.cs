using System;

namespace SalesTracker.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int UserId { get; set; }  // FK prema User

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
