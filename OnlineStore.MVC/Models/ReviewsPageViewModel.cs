using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Models.Review;

namespace OnlineStore.MVC.Models
{
    public class ReviewsPageViewModel
    {
        public IEnumerable<OrderViewModel> OrdersAwaitingReview { get; set; } = Enumerable.Empty<OrderViewModel>();

        public IEnumerable<ReviewViewModel> Reviews { get; set; } = Enumerable.Empty<ReviewViewModel>();

        public bool IsEmpty => !OrdersAwaitingReview.Any() && !Reviews.Any();
    }
}
