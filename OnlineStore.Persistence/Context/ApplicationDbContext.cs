using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces;
using OnlineStore.DAL.EntityTypeConfigurations;
using OnlineStore.Domain.Entities;
using OnlineStore.Persistence.EntityTypeConfigurations;

namespace OnlineStore.DAL.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SpecificationType> SpecificationTypes { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<NestedMenuItem> NestedMenuItems { get; set; }
        public DbSet<FiltersGroup> FilterGoups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new SpecificationConfiguration());
            builder.ApplyConfiguration(new SpecificationTypeConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderItemConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new WishlistConfiguration());
            builder.ApplyConfiguration(new ContactRequestConfiguration());
            builder.ApplyConfiguration(new CouponConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new SubscriberConfiguration());
            builder.ApplyConfiguration(new MenuItemConfiguration());
            builder.ApplyConfiguration(new NestedMenuItemConfiguration());
            builder.ApplyConfiguration(new FiltersGroupConfiguration());
            base.OnModelCreating(builder);
        }
    }
}