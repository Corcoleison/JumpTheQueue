using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class Queue
    {
        public Queue()
        {
            AccessCode = new HashSet<AccessCode>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Accesslink { get; set; }
        public int? Minattentiontime { get; set; }
        public string Opentime { get; set; }
        public string Closetime { get; set; }
        public bool? Started { get; set; }
        public bool? Closed { get; set; }
        public string UserClientid { get; set; }

        public virtual User UserClient { get; set; }
        public virtual ICollection<AccessCode> AccessCode { get; set; }
    }
}
