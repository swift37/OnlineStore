﻿namespace OnlineStore.MVC.Models.Coupon
{
    public class CouponViewModel
    {
        public int Id { get; set; }

        public string? Number { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? FinishDate { get; set; }

        public decimal DiscountSize { get; set; }

        public double PercentDiscountSize { get; set; }

        public int MaxUsesCount { get; set; }

        public int CurrentUsesCount { get; set; }

        public bool IsNotUsesLimit { get; set; }

        public bool IsActive { get; set; }
    }
}
