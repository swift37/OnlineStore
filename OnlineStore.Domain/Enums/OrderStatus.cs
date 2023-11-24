using OnlineStore.Domain.Enums.Atributes;

namespace OnlineStore.Domain.Enums
{
    public enum OrderStatus
    {
        [StringValue("Not paid")]
        NotPaid,
        [StringValue("Paid")]
        Paid,
        [StringValue("Processed")]
        Processed,
        [StringValue("Completed")]
        Completed,
        [StringValue("Canceled")]
        Canceled
    }
}
