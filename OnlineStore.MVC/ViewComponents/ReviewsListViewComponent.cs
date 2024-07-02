using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class ReviewsListViewComponent : ViewComponent
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsListViewComponent(IReviewsService reviewsService) =>
            _reviewsService = reviewsService;

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var response = await _reviewsService.GetReviewsByProduct(productId);
            var model = response.Data;

            return View(model);
        }
    }
}
