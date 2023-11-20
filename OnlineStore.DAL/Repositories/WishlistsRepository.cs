﻿using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Context;
using OnlineStore.Domain;

namespace OnlineStore.DAL.Repositories
{
    public class WishlistsRepository : Repository<Wishlist>, IWishlistsRepository
    {
        public WishlistsRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Wishlist> GetUserWishlistAsync(
            Guid userId, 
            CancellationToken cancellation = default) => await Entities
            .FirstOrDefaultAsync(o => o.UserId == userId, cancellation)
            .ConfigureAwait(false) 
            ?? throw new NotFoundException(nameof(Wishlist), string.Empty);
    }
}