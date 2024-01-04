using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;

namespace OnlineStore.Application.DTOs.Specification
{
    public class UpdateSpecificationDTO : BaseDTO
    {
        public int SpecificationTypeId { get; set; }

        public string? Value { get; set; }

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();
    }
}
