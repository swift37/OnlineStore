namespace OnlineStore.MVC.Models.Product
{
    public class CreateProductViewModel
    {
        public string? Name { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CreateSpecificationViewModel> Specifications { get; set; } = new HashSet<CreateSpecificationViewModel>();

        public string? Manufacturer { get; set; }

        public string? ManufacturersCode { get; set; }

        public string? StoreCode { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsNewProduct { get; set; }

        public bool IsSale { get; set; }

        public bool IsFeaturedProduct { get; set; }
    }
}
