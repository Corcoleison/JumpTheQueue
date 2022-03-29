using System;
using System.Collections.Generic;

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class AccessCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public TimeSpan? Createdtime { get; set; }
        public TimeSpan? Endtime { get; set; }
        public Status_t Status { get; set; }
        public Guid VisitorUid { get; set; }
        public int QueueId { get; set; }
        public TimeSpan? StartTime { get; set; }

        public virtual Queue Queue { get; set; }
        public virtual Visitor VisitorU { get; set; }
    }
    
    public enum Status_t
    {
        notStarted,
        waiting,
        attending,
        attended,
        skipped
    }
}
