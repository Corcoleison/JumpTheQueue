using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto
{
    /// <summary>
    /// Visitor definition
    /// </summary>
    public class QueueDto
    {
        /// <summary>
        /// the UID of the visitor
        /// </summary>
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
    }
}
