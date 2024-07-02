using OnlineStore.MVC.Models.Specification;

namespace OnlineStore.MVC.Models.SpecificationType
{
    public class SpecificationTypeViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public bool IsMain { get; set; }

        public ICollection<SpecificationViewModel> Values { get; set; } = new HashSet<SpecificationViewModel>();
    }
}
