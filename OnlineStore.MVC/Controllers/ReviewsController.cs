﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models.Review;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService) => _reviewsService = reviewsService;

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _reviewsService.GetAll();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _reviewsService.Get(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Exist(int id)
        {
            var response = await _reviewsService.Exist(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var model = new CreateReviewViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _reviewsService.Create(model);

            if (response.Success)
                return RedirectToAction("GetAll");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _reviewsService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(ReviewViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _reviewsService.Update(model);

            if (response.Success)
                return RedirectToAction("GetAll");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _reviewsService.Delete(id);

            if (response.Success) return RedirectToAction("GetAll");

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> GetReviewsByProduct(int productId)
        {
            var response = await _reviewsService.GetReviewsByProduct(productId);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }
    }
}
