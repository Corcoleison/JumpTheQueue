using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation
{
    public partial class Visitor
    {
        public Visitor()
        {
            AccessCode = new HashSet<AccessCode>();
        }

        public string Uid { get; set; }

        public virtual ICollection<AccessCode> AccessCode { get; set; }
    }
}
