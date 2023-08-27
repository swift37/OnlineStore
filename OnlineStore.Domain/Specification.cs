using OnlineStore.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public class Specification : Entity
    {
        public string? SpecificationName { get; set; }

        public string? SpecificationValue { get; set; }

        public bool IsMain { get; set; }
    }
}
