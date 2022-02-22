using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto
{
    /// <summary>
    /// Visitor definition
    /// </summary>
    public class AccessCodeDto
    {
        /// <summary>
        /// the UID of the visitor
        /// </summary>
        public int Id { get; set; }
        public string Code { get; set; }
        public string Createdtime { get; set; }
        public string Endtime { get; set; }
        public string Status { get; set; }
        public Guid VisitorUid { get; set; }
        public int QueueId { get; set; }
    }
}
