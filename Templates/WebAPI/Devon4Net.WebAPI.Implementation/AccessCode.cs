using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation
{
    public partial class AccessCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public TimeSpan? Createdtime { get; set; }
        public TimeSpan? Endtime { get; set; }
        public string VisitorUid { get; set; }
        public int? QueueId { get; set; }

        public virtual Queue Queue { get; set; }
        public virtual Visitor VisitorU { get; set; }
    }
}
