using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Queue = new HashSet<Queue>();
        }

        public string Clientid { get; set; }
        public Role_t Role { get; set; }

        public virtual ICollection<Queue> Queue { get; set; }
    }

    public enum Role_t
    {
        Owner,
        Employee
    }
}
