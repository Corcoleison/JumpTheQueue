using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Converters
{
    /// <summary>
    /// TodoConverter
    /// </summary>
    public static class AccessCodeConverter
    {
        /// <summary>
        /// ModelToDto TODO transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static AccessCodeDto ModelToDto(AccessCode item)
        {
            if (item == null) return new AccessCodeDto();

            return new AccessCodeDto
            {
                Id = item.Id,
                Code = item.Code,
                Createdtime = item.Createdtime.HasValue ? item.Createdtime.Value.ToString(@"hh\:mm") : "<not available>",
                Endtime = item.Endtime.HasValue ? item.Endtime.Value.ToString(@"hh\:mm") : "<not available>",
                Status = item.Status.ToString(),
                VisitorUid = item.VisitorUid,
                QueueId = item.QueueId


            };
        }

    }
}
