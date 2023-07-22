using OnlineStore.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public class ProductDetails : Entity
    {
        public string? PropertyName { get; set; }

        public string? PropertyValue { get; set; }
    }
}
