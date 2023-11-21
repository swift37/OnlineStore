using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;

namespace OnlineStore.Application.DTOs.Review
{
    public class ReviewDTO: BaseDTO
    {
        public ProductDTO? Product { get; set; }

        public DateTime CreationDate { get; set; }

        public string? Title { get; set; }

        public double Rating { get; set; }

        public string? Content { get; set; }
    }
}
