using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;

namespace OnlineStore.Application.DTOs.Review
{
    public class ReviewDTO: BaseDTO
    {
        public int ProductId {  get; set; } 

        public ProductDTO? Product { get; set; }

        public DateTime CreationDate { get; set; }

        public string? Title { get; set; }

        public int Rating { get; set; }

        public string? Content { get; set; }
    }
}
