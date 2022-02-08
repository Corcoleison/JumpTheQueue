using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Converters
{
    /// <summary>
    /// TodoConverter
    /// </summary>
    public static class VisitorConverter
    {
        /// <summary>
        /// ModelToDto TODO transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static VisitorDto ModelToDto(Visitor item)
        {
            if (item == null) return new VisitorDto();

            return new VisitorDto
            {
                Uid = item.Uid,
            };
        }

    }
}
