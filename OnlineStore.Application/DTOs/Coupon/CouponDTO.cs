using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Coupon
{
    public class CouponDTO : BaseDTO, IMapWith<Domain.Entities.Coupon>
    {
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

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Coupon, CouponDTO>().ReverseMap();
    }
}
