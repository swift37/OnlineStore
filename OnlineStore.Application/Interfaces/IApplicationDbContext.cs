using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SpecificationType> SpecificationTypes { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<NestedMenuItem> NestedMenuItems { get; set; }
        public DbSet<FiltersGroup> FilterGroups { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class; 

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
