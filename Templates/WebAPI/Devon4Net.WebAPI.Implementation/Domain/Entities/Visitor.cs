using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class Visitor
    {
        public Visitor()
        {
            AccessCode = new HashSet<AccessCode>();
        }

        public Guid Uid { get; set; }

        public virtual ICollection<AccessCode> AccessCode { get; set; }
    }
}
