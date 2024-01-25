namespace OnlineStore.Application.Models
{
    public class PaymentStatusResponse
    {
        public required string OrderNumber { get; set; }

        public bool IsPaid { get; set; }

        public bool IsFailed { get; set; }

        public bool IsInProcess { get; set; }
    }
}
