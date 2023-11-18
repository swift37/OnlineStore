﻿using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IWishlistsRepository : IRepository<Wishlist>
    {
        Task<Wishlist> GetUserWishlistAsync(
            Guid userId, 
            CancellationToken cancellation = default);
    }
}
