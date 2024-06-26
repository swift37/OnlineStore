﻿using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class ContactRequest : Entity
    {
        public string? ContactName { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset? ResponseDate { get; set; }

        public bool IsConsidered { get; set; }
    }
}
