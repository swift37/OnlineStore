using OnlineStore.MVC.Models.Enums.Atributes;

namespace OnlineStore.MVC.Models.Enums
{
    public enum OrderStatus
    {
        [StringValue("Not paid")]
        NotPaid,
        [StringValue("Paid")]
        Paid,
        [StringValue("Processed")]
        Processed,
        [StringValue("Shipped")]
        Shipped,
        [StringValue("Completed")]
        Completed,
        [StringValue("Canceled")]
        Canceled
    }
}
