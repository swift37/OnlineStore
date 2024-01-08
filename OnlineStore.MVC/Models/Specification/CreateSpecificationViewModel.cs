using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Specification
{
    public class CreateSpecificationViewModel
    {
        public int SpecificationTypeId { get; set; }

        public string? Value { get; set; }

        public ICollection<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();
    }
}
