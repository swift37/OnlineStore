using OnlineStore.MVC.Models.Specification;

namespace OnlineStore.MVC.Models.SpecificationType
{
    public class CreateSpecificationTypeViewModel
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public bool IsMain { get; set; }

        public ICollection<CreateSpecificationViewModel> Values { get; set; } = new HashSet<CreateSpecificationViewModel>();
    }
}
