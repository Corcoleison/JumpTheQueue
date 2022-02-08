using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation
{
    public partial class User
    {
        public User()
        {
            Queue = new HashSet<Queue>();
        }

        public string Clientid { get; set; }

        public virtual ICollection<Queue> Queue { get; set; }
    }
}
