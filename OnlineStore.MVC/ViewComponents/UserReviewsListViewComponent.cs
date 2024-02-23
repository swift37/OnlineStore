using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class UserReviewsListViewComponent : ViewComponent
    {
        private readonly IReviewsService _reviewsService;

        public UserReviewsListViewComponent(IReviewsService reviewsService) =>
            _reviewsService = reviewsService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _reviewsService.GetUserReviews();
            var model = response.Data;
            return View(model);
        }
    }
}
