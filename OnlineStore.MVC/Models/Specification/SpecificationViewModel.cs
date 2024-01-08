using OnlineStore.MVC.Models.Product;
using OnlineStore.MVC.Models.SpecificationType;

namespace OnlineStore.MVC.Models.Specification
{
    public class SpecificationViewModel
    {
        public int Id { get; set; }

        public int SpecificationTypeId { get; set; }

        public SpecificationTypeViewModel? SpecificationType { get; set; }

        public string? Value { get; set; }

        public ICollection<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();

        public int ProductsCount { get; set; }
    }
}
