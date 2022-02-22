using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Converters
{
    /// <summary>
    /// TodoConverter
    /// </summary>
    public static class QueueConverter
    {
        /// <summary>
        /// ModelToDto TODO transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static QueueDto ModelToDto(Queue item)
        {
            if (item == null) return new QueueDto();

            return new QueueDto
            {
                Id = item.Id,
                Name = item.Name,
                Logo = item.Logo,
                Accesslink = item.Accesslink,
                Minattentiontime = item.Minattentiontime,
                Opentime = item.Opentime,
                Closetime = item.Closetime,
                Started = item.Started,
                Closed = item.Closed,
                UserClientid = item.UserClientid
            };
        }

    }
}
