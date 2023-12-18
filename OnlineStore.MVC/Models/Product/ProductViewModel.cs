using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Models.Review;

namespace OnlineStore.MVC.Models.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public CategoryViewModel? Category { get; set; }

        public ICollection<SpecificationViewModel> Specifications { get; set; } = new HashSet<SpecificationViewModel>();

        public double Rating { get; set; }

        public ICollection<ReviewViewModel> Reviews { get; set; } = new HashSet<ReviewViewModel>();

        public string? Manufacturer { get; set; }

        public string? ManufacturersCode { get; set; }

        public string? StoreCode { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsNewProduct { get; set; }

        public bool IsSale { get; set; }

        public bool IsFeaturedProduct { get; set; }


        public decimal PriceAfterDiscount => UnitPrice - Discount;
    }
}
