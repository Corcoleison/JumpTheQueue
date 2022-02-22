using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Converters
{
    /// <summary>
    /// TodoConverter
    /// </summary>
    public static class UserConverter
    {
        /// <summary>
        /// ModelToDto TODO transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static UserDto ModelToDto(User itemUser)
        {
            if (itemUser == null) return new UserDto();

            return new UserDto
            {
                Clientid = itemUser.Clientid,
                Role = itemUser.Role.ToString(),
            };
        }

    }
}
