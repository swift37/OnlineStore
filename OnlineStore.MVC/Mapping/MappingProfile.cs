using AutoMapper;
using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Models.ContactRequest;
using OnlineStore.MVC.Models.Coupon;
using OnlineStore.MVC.Models.Event;
using OnlineStore.MVC.Models.FiltersGroup;
using OnlineStore.MVC.Models.MenuItem;
using OnlineStore.MVC.Models.NestedMenuItem;
using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Models.Product;
using OnlineStore.MVC.Models.Review;
using OnlineStore.MVC.Models.Specification;
using OnlineStore.MVC.Models.SpecificationType;
using OnlineStore.MVC.Models.Subscriber;
using OnlineStore.MVC.Models.Wishlist;
using OnlineStore.MVC.Services.ApiClient;

namespace OnlineStore.MVC.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<UpdateCategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<CreateCategoryDTO, CreateCategoryViewModel>().ReverseMap();

            CreateMap<ContactRequestDTO, ContactRequestViewModel>().ReverseMap();
            CreateMap<UpdateContactRequestDTO, ContactRequestViewModel>().ReverseMap();
            CreateMap<CreateContactRequestDTO, CreateContactRequestViewModel>().ReverseMap();

            CreateMap<CouponDTO, CouponViewModel>().ReverseMap();
            CreateMap<UpdateCouponDTO, CouponViewModel>().ReverseMap();
            CreateMap<CreateCouponDTO, CreateCouponViewModel>().ReverseMap();

            CreateMap<EventDTO, EventViewModel>().ReverseMap();
            CreateMap<UpdateEventDTO, EventViewModel>().ReverseMap();
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
            CreateMap<UpdateOrderDTO, OrderViewModel>()
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

            CreateMap<Services.ApiClient.StripePaymentRequest, Models.StripePaymentRequest>()
                .ReverseMap();
            CreateMap<Services.ApiClient.PaymentSessionResponse, Models.PaymentSessionResponse>()
                .ReverseMap();
            CreateMap<Services.ApiClient.PaymentStatusResponse, Models.PaymentStatusResponse>()
                .ReverseMap();

            CreateMap<ProductDTO, ProductViewModel>().ReverseMap();
            CreateMap<UpdateProductDTO, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductDTO, CreateProductViewModel>().ReverseMap();

            CreateMap<SpecificationDTO, SpecificationViewModel>().ReverseMap();
            CreateMap<UpdateSpecificationDTO, SpecificationViewModel>().ReverseMap();
            CreateMap<CreateSpecificationDTO, CreateSpecificationViewModel>().ReverseMap();

            CreateMap<SpecificationTypeDTO, SpecificationTypeViewModel>().ReverseMap();
            CreateMap<UpdateSpecificationTypeDTO, SpecificationTypeViewModel>().ReverseMap();
            CreateMap<CreateSpecificationTypeDTO, CreateSpecificationTypeViewModel>().ReverseMap();

            CreateMap<ProductsPageDTO, ProductsPageViewModel>();

            CreateMap<ReviewDTO, ReviewViewModel>().ReverseMap();
            CreateMap<UpdateReviewDTO, ReviewViewModel>().ReverseMap();
            CreateMap<CreateReviewDTO, CreateReviewViewModel>().ReverseMap();

            CreateMap<SubscriberDTO, SubscriberViewModel>().ReverseMap();
            CreateMap<UpdateSubscriberDTO, SubscriberViewModel>().ReverseMap();
            CreateMap<CreateSubscriberDTO, CreateSubscriberViewModel>().ReverseMap();

            CreateMap<WishlistDTO, WishlistViewModel>().ReverseMap();
            CreateMap<UpdateWishlistDTO, WishlistViewModel>().ReverseMap();
            CreateMap<CreateWishlistDTO, CreateWishlistViewModel>().ReverseMap();

            CreateMap<WishlistDTO, WishlistViewModel>().ReverseMap();
            CreateMap<UpdateWishlistDTO, WishlistViewModel>().ReverseMap();
            CreateMap<CreateWishlistDTO, CreateWishlistViewModel>().ReverseMap();
            CreateMap<WishlistItemDTO, WishlistItemViewModel>().ReverseMap();
            CreateMap<UpdateWishlistItemDTO, WishlistItemViewModel>().ReverseMap();
            CreateMap<CreateWishlistItemDTO, CreateWishlistItemViewModel>().ReverseMap();

            CreateMap<MenuItemDTO, MenuItemViewModel>()
                .ForMember(dest => dest.NavigationLabel, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NavigationLabel));
            CreateMap<UpdateMenuItemDTO, MenuItemViewModel>()
                .ForMember(dest => dest.NavigationLabel, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NavigationLabel));
            CreateMap<CreateMenuItemDTO, CreateMenuItemViewModel>()
                .ForMember(dest => dest.NavigationLabel, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NavigationLabel));
            CreateMap<NestedMenuItemDTO, NestedMenuItemViewModel>()
                .ForMember(dest => dest.NavigationLabel, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NavigationLabel));
            CreateMap<UpdateNestedMenuItemDTO, UpdateNestedMenuItemViewModel>()
                .ForMember(dest => dest.NavigationLabel, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NavigationLabel));
            CreateMap<CreateNestedMenuItemDTO, CreateNestedMenuItemViewModel>()
                .ForMember(dest => dest.NavigationLabel, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NavigationLabel));

            CreateMap<ProductsFilteringOptionsDTO, ProductsFilteringOptions>()
                .ForMember(dest => dest.SortBy,
                    opt => opt.MapFrom(src => (Models.Enums.SortParameter)(int)src.SortBy))
                .ReverseMap()
                .ForMember(dest => dest.SortBy,
                    opt => opt.MapFrom(src => (SortParameter)(int)src.SortBy));

            CreateMap<FiltersGroupDTO, FiltersGroupViewModel>().ReverseMap();
            CreateMap<UpdateFiltersGroupDTO, UpdateFiltersGroupViewModel>().ReverseMap();
            CreateMap<CreateFiltersGroupDTO, CreateFiltersGroupViewModel>().ReverseMap();

            CreateMap<FiltersGroupOptions, FiltersGroupOptionsDTO>().ReverseMap();

            CreateMap<LoginViewModel, LoginRequest>();
            CreateMap<RegisterViewModel, RegisterRequest>();
            CreateMap<Models.RefreshRequest, Services.ApiClient.RefreshRequest>();
            CreateMap<UpdateUserViewModel, UpdateUserRequest>();
            CreateMap<ChangeEmailViewModel, ChangeEmailRequest>();
            CreateMap<ChangePasswordViewModel, ChangePasswordRequest>();
            CreateMap<ResetPasswordViewModel, ResetPasswordRequest>();
        }
    }
}
