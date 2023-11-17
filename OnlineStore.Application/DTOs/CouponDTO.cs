namespace OnlineStore.Application.DTOs
{
    public class CouponDTO
    {
        public int Id { get; set; }

        public string? Number { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public decimal DiscountSize { get; set; }

        public double PercentDiscountSize { get; set; }

        public int MaxUsesCount { get; set; }

        public int CurrentUsesCount { get; set; }

        public bool IsNotUsesLimit { get; set; }

        public bool IsActive { get; set; }
    }
}
