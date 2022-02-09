using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto
{
    /// <summary>
    /// Visitor definition
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// the UID of the visitor
        /// </summary>
        public string Clientid { get; set; }
        public Role_t Role { get; set; }
    }
}
