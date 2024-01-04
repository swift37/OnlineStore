using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.DTOs.SpecificationType;

namespace OnlineStore.Application.DTOs.Specification
{
    public class SpecificationDTO : BaseDTO
    {
        public int SpecificationTypeId { get; set; }

        public SpecificationTypeDTO? SpecificationType { get; set; }

        public string? Value { get; set; }

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();

        public int ProductsCount { get; set; }
    }
}
