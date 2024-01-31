using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Models.Review;

namespace OnlineStore.MVC.Models
{
    public class ReviewsPageViewModel
    {
        public IEnumerable<OrderViewModel> OrdersAwaitingReview { get; set; } = Enumerable.Empty<OrderViewModel>();

        public IEnumerable<ReviewViewModel> Reviews { get; set; } = Enumerable.Empty<ReviewViewModel>();

        public bool HasOrdersAwaitingReview => OrdersAwaitingReview.Any(o => o.Items.Any());

        public bool HasReviews => Reviews.Any();

        public bool IsEmpty => !HasOrdersAwaitingReview && !HasReviews;
    }
}
