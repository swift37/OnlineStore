﻿using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}