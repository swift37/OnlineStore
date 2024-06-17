using AutoMapper;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.PaymentMethod
{
    public class CreatePaymentMethodDTO : IMapWith<Domain.Entities.PaymentMethod>
    {
        public string? DisplayName { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public bool IsAvailable { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.PaymentMethod, CreatePaymentMethodDTO>().ReverseMap();
    }
}
