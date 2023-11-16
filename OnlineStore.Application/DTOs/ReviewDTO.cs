using OnlineStore.Domain;

namespace OnlineStore.Application.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        public ProductDTO? Product { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string? Title { get; set; }

        public double Rating { get; set; }

        public string? Content { get; set; }
    }
}
