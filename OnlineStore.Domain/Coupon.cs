﻿using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Coupon : Entity
    {
        public string? Number { get; set; }
        
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public DateTime StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public decimal DiscountSize { get; set; }

        public double PercentDiscountSize { get; set; }

        public int MaxUsesCount { get; set; }

        public int CurrentUsesCount { get; set; }

        public bool IsNotUsesLimit { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
