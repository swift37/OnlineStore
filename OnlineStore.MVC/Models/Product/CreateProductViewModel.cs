﻿using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Models.ProductTag;
using OnlineStore.MVC.Models.Specification;

namespace OnlineStore.MVC.Models.Product
{
    public class CreateProductViewModel
    {
        public string? Name { get; set; }

        public decimal UnitCost { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public ICollection<SpecificationViewModel> Specifications { get; set; } = new HashSet<SpecificationViewModel>();

        public ICollection<ProductTagViewModel> Tags { get; set; } = new HashSet<ProductTagViewModel>();

        public string? Manufacturer { get; set; }

        public string? ManufacturersCode { get; set; }

        public string? StoreCode { get; set; }

        public ProductStatus Status { get; set; }

        public ProductAvailability Availability { get; set; }
    }
}
