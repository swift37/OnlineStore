using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Coupon
{
    public class UpdateCouponDTO : BaseDTO, IMapWith<Domain.Entities.Coupon>
    {
        public string? Number { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public decimal DiscountSize { get; set; }

        public double PercentDiscountSize { get; set; }

        public int MaxUsesCount { get; set; }

        public int CurrentUsesCount { get; set; }

        public bool IsNotUsesLimit { get; set; }

        public bool IsActive { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Coupon, UpdateCouponDTO>().ReverseMap();
    }
}
