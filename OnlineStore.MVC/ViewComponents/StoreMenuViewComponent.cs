﻿using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class StoreMenuViewComponent : ViewComponent
    {
        private readonly ICategoriesService _categoriesService;

        public StoreMenuViewComponent(ICategoriesService categoriesService) =>
            _categoriesService = categoriesService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _categoriesService.GetMainCategories();
            var model = response.Data;

            return View(model);
        }
    }
}
