using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain
{
    public class Coupon : Entity
    {
        public string? Number { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountSize { get; set; }

        public double PercentDiscountSize { get; set; }

        public int MaxUsesCount { get; set; }

        public int CurrentUsesCount { get; set; }

        public bool IsNotUsesLimit { get; set; }

        public bool IsActive { get; set; } = true;

        [NotMapped]
        public bool IsActual => IsActive && DateTime.Now >= StartDate && DateTime.Now <= FinishDate && (IsNotUsesLimit || CurrentUsesCount < MaxUsesCount);
    }
}
