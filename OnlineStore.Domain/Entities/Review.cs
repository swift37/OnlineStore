using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Domain.Entities
{
    public class Review : Entity
    {
        public Guid? UserId { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int? OrderId { get; set; }

        public Order? Order { get; set; }

        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset LastChangeDate { get; set; } = DateTimeOffset.Now;

        public string? Name { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Content { get; set; }

        public bool IsModerated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
