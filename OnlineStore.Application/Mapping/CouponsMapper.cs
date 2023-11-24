using OnlineStore.Application.DTOs.Coupon;
using OnlineStore.Domain.Entities;

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

        public static Coupon FromDTO(this CreateCouponDTO coupon) => new Coupon
        {
            Number = coupon.Number,
            CreationDate = coupon.CreationDate,
            StartDate = coupon.StartDate,
            FinishDate = coupon.FinishDate,
            DiscountSize = coupon.DiscountSize,
            PercentDiscountSize = coupon.PercentDiscountSize,
            MaxUsesCount = coupon.MaxUsesCount,
            IsNotUsesLimit = coupon.IsNotUsesLimit,
            IsActive = coupon.IsActive
        };

        public static Coupon FromDTO(this UpdateCouponDTO coupon) => new Coupon
        {
            Id = coupon.Id,
            Number = coupon.Number,
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
