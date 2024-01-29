namespace OnlineStore.MVC.Models
{
    public class StripePaymentRequest
    {
        public required string SuccessUrl { get; set; }

        public required string CancelUrl { get; set; }

        public required string OrderNumber { get; set; }
    }
}
