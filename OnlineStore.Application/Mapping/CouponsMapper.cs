﻿using OnlineStore.Application.DTOs;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class CouponsMapper
    {
        public static CouponDTO ToDTO(this Coupon coupon) => new CouponDTO
        {
            Id = coupon.Id,
            Number = coupon.Number,
            CreationDate = coupon.CreationDate,
            StartDate = coupon.StartDate,
            FinishDate = coupon.FinishDate,
            DiscountSize = coupon.DiscountSize,
            PercentDiscountSize = coupon.PercentDiscountSize,
            MaxUsesCount = coupon.MaxUsesCount,
            CurrentUsesCount = coupon.CurrentUsesCount,
            IsNotUsesLimit = coupon.IsNotUsesLimit,
            IsActive = coupon.IsActive
        };

        public static Coupon FromDTO(this CouponDTO coupon) => new Coupon
        {
            Id = coupon.Id,
            Number = coupon.Number,
            CreationDate = coupon.CreationDate,
            StartDate = coupon.StartDate,
            FinishDate = coupon.FinishDate,
            DiscountSize = coupon.DiscountSize,
            PercentDiscountSize = coupon.PercentDiscountSize,
            MaxUsesCount = coupon.MaxUsesCount,
            CurrentUsesCount = coupon.CurrentUsesCount,
            IsNotUsesLimit = coupon.IsNotUsesLimit,
            IsActive = coupon.IsActive
        };

        public static IEnumerable<CouponDTO> ToDTO(this IEnumerable<Coupon> coupons) => coupons.Select(c => c.ToDTO());

        public static IEnumerable<Coupon> FromDTO(this IEnumerable<CouponDTO> coupons) => coupons.Select(c => c.FromDTO());
    }
}