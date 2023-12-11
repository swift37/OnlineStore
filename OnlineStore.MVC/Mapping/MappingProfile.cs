using AutoMapper;
using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Models.ContactRequest;
using OnlineStore.MVC.Models.Coupon;
using OnlineStore.MVC.Models.Event;
using OnlineStore.MVC.Models.MenuItem;
using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Models.Product;
using OnlineStore.MVC.Models.Review;
using OnlineStore.MVC.Models.Subscriber;
using OnlineStore.MVC.Models.Wishlist;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.WebAPI.Models;

namespace OnlineStore.MVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<CreateCategoryDTO, CreateCategoryViewModel>().ReverseMap();

            CreateMap<ContactRequestDTO, ContactRequestViewModel>().ReverseMap();
            CreateMap<CreateContactRequestDTO, CreateContactRequestViewModel>().ReverseMap();

            CreateMap<CouponDTO, CouponViewModel>().ReverseMap();
            CreateMap<CreateCouponDTO, CreateCouponViewModel>().ReverseMap();

            CreateMap<EventDTO, EventViewModel>().ReverseMap();
            CreateMap<CreateEventDTO, CreateEventViewModel>().ReverseMap();

            // Cast Models.Enums.OrderStatus to ApiClient.OrderStatus and vice versa
            // to separate automatically generated models from controllers and views.
            // Leaving auto-generated models only inside the services.
            CreateMap<OrderDTO, OrderViewModel>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => (Models.Enums.OrderStatus)(int)src.Status))
                .ReverseMap()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => (OrderStatus)(int)src.Status));
            CreateMap<CreateOrderDTO, CreateOrderViewModel>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => (Models.Enums.OrderStatus)(int)src.Status))
                .ReverseMap()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => (OrderStatus)(int)src.Status));
            CreateMap<OrderItemDTO, OrderItemViewModel>().ReverseMap();
            CreateMap<CreateOrderItemDTO, CreateOrderItemViewModel>().ReverseMap();

            CreateMap<ProductDTO, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductDTO, CreateProductViewModel>().ReverseMap();
            CreateMap<SpecificationDTO, SpecificationViewModel>().ReverseMap();
            CreateMap<CreateSpecificationDTO, CreateSpecificationViewModel>().ReverseMap();
            CreateMap<ProductsPageDTO, ProductsPageViewModel>();

            CreateMap<ReviewDTO, ReviewViewModel>().ReverseMap();
            CreateMap<CreateReviewDTO, CreateReviewViewModel>().ReverseMap();

            CreateMap<SubscriberDTO, SubscriberViewModel>().ReverseMap();
            CreateMap<CreateSubscriberDTO, CreateSubscriberViewModel>().ReverseMap();

            CreateMap<WishlistDTO, WishlistViewModel>().ReverseMap();
            CreateMap<CreateWishlistDTO, CreateWishlistViewModel>().ReverseMap();

            CreateMap<WishlistDTO, WishlistViewModel>().ReverseMap();
            CreateMap<CreateWishlistDTO, CreateWishlistViewModel>().ReverseMap();

            CreateMap<MenuItemDTO, MenuItemViewModel>()
                .ForMember(dest => dest.NavigationLabel, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NavigationLabel));
            CreateMap<NestedMenuItemDTO, NestedMenuItemViewModel>()
                .ForMember(dest => dest.NavigationLabel, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NavigationLabel)); 

            CreateMap<LoginViewModel, LoginRequest>();
            CreateMap<RegisterViewModel, RegisterRequest>();
        }
    }
}
