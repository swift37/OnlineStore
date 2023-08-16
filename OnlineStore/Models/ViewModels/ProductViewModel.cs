﻿using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace OnlineStore.Models.ViewModels
{

    public class ProductViewModel : Product
    {
        public IEnumerable<SelectListItem> AvailableCategories { get; set; } = new HashSet<SelectListItem>();

        public IEnumerable<SelectListItem> AvailableSubCategories { get; set; } = new HashSet<SelectListItem>();

        public IFormFile? ImageFile { get; set; }
    }
}