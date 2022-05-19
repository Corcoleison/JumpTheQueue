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
                Createdtime = (item.Createdtime?.ToString(@"hh\:mm")),
                StartTime = (item.StartTime?.ToString(@"hh\:mm")),
                Endtime = (item.Endtime?.ToString(@"hh\:mm")),
                Status = item.Status.ToString(),
                VisitorUid = item.VisitorUid,
                QueueId = item.QueueId


            };
        }

    }
}
