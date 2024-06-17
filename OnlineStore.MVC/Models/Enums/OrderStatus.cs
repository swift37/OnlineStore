using OnlineStore.MVC.Models.Enums.Atributes;

namespace OnlineStore.MVC.Models.Enums
{
    public enum OrderStatus
    {
        [StringValue("To Pay")]
        ToPay,
        [StringValue("Pending")]
        Pending,
        [StringValue("In-Progress")]
        InProgress,
        [StringValue("Shipped")]
        Shipped,
        [StringValue("Completed")]
        Completed,
        [StringValue("Canceled")]
        Canceled,
        [StringValue("Returned")]
        Returned
    }
}
