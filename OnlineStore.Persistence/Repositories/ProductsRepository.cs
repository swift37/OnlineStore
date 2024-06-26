﻿using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enums;

namespace OnlineStore.DAL.Repositories
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        protected override IQueryable<Product> Entities => base.Entities
            .Include(product => product.Category)
            .Include(product => product.Tags)
            .Include(product => product.Specifications)
                .ThenInclude(specification => specification.SpecificationType)
            .AsNoTracking();

        public ProductsRepository(IApplicationDbContext context) : base(context) { }

        public async Task<ProductsPage> GetFilteredProductsAsync(
            ProductsFilteringOptions options,
            CancellationToken cancellation = default)
        {
            if (options.ItemsPerPage > 30 || options.ItemsPerPage < 15) options.ItemsPerPage = 15;

            var category = await _context.Categories
                .SingleOrDefaultAsync(c => c.Id == options.CategoryId, cancellation)
                .ConfigureAwait(false);

            if (category is null) throw new NotFoundException(nameof(Category), options.CategoryId);
            var specificationIds = options.SpecificationIds.Values.SelectMany(v => v).ToList();
            var query = Entities.Where(product =>
                product.CategoryId == options.CategoryId &&
                product.UnitPrice - product.Discount >= options.MinPrice &&
                product.UnitPrice - product.Discount <= options.MaxPrice &&
                product.Specifications.Select(spec => spec.SpecificationTypeId)
                .Intersect(options.SpecificationIds.Keys)
                .Count() == options.SpecificationIds.Keys.Count() &&
                product.Specifications.Select(spec => spec.Id)
                .Intersect(specificationIds)
                .Count() >=
                product.Specifications.Select(spec => spec.SpecificationTypeId)
                .Intersect(options.SpecificationIds.Keys)
                .Count());

            var pagesCount = 
                (await query.CountAsync(cancellation) + options.ItemsPerPage - 1) 
                / options.ItemsPerPage;

            var productsCollection = await query
                .OrderBy(p => p.Availability)
                .Skip((options.PageNumber - 1) * options.ItemsPerPage)
                .Take(options.ItemsPerPage)
                .Include(p => p.Category)
                .ToArrayAsync();

            var products = new ProductsPage
            {
                Products = productsCollection,
                Category = category,
                CurrentPage = options.PageNumber,
                TotalPages = pagesCount,
                ItemsPerPage = options.ItemsPerPage
            };

            return products;
        }

        public async Task<int> GetCountByFilterAsync(
            int categoryId, 
            int specificationId, 
            CancellationToken cancellation = default) => await Entities
            .Where(p => p.CategoryId == categoryId && p.Specifications
                .Any(s => s.Id == specificationId))
            .CountAsync(cancellation)
            .ConfigureAwait(false);

        public async Task<IEnumerable<Product>> GetAllByTagAsync(
            int tagId,
            CancellationToken cancellation = default) => await Entities
            .Where(p => p.Tags.Any(tag => tag.Id == tagId))
            .ToArrayAsync()
            .ConfigureAwait(false);

        public async Task<IEnumerable<Product>> GetAllByTagAsync(
            string tagName,
            CancellationToken cancellation = default) => await Entities
            .Where(p => p.Tags.Any(tag => tag.Name == tagName))
            .ToArrayAsync()
            .ConfigureAwait(false);

        public async Task<IEnumerable<Product>> GetAllByAvailabilityAsync(
            ProductAvailability productAvailability,
            CancellationToken cancellation = default) => await Entities
            .Where(p => p.Availability == productAvailability)
            .ToArrayAsync()
            .ConfigureAwait(false);

        public async Task<IEnumerable<Product>> GetAllByStatusAsync(
            ProductStatus productStatus,
            CancellationToken cancellation = default) => await Entities
            .Where(p => p.Status == productStatus)
            .ToArrayAsync()
            .ConfigureAwait(false);
    }
}
