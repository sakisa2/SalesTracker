using System;

namespace SalesTracker.Models
{
    public class Goal
    {
        public int Id { get; set; }

        public int UserId { get; set; }  // fk prema user

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal TargetAmount { get; set; }
    }
}
