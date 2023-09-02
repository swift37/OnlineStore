using OnlineStore.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public class Review : Entity
    {
        public ApplicationUser? User { get; set; }

        public Product? Product { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string? Title { get; set; }

        [Range(0.0, 5.0)]
        public double Rating { get; set; }

        public string? Content { get; set; }

        public bool IsModerated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
