using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
