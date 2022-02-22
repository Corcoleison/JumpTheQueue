using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Service
{
    /// <summary>
    /// IAccessCodeService
    /// </summary>
    public interface IAccessCodeService
    {
        /// <summary>
        /// GetAccessCode
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<AccessCodeDto>> GetAccessCode(Expression<Func<AccessCode, bool>> predicate = null);

        /// <summary>
        /// GetAccessCodeById
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<AccessCode> GetAccessCodeByCode(string code);

        /// <summary>
        /// Gets the AccessCode by queue id
        /// </summary>
        /// <param name="queueid"></param>
        /// <param name="visitoruid"></param>
        /// <returns></returns>
        Task<AccessCode> GetAccessCodeByVisitorAndQueue(string visitoruid, int? queueid);

        /// <summary>
        /// Gets the LAST AccessCode by queue id
        /// </summary>
        /// <param name="queueid"></param>
        /// <returns></returns>
        Task<AccessCode> GetLastAccessCodeByQueue(int? queueid);

        /// <summary>
        /// CreateAccessCode
        /// </summary>
        /// <param name="visitoruid"></param>
        /// <param name="queueId"></param>
        /// <returns></returns>
        Task<AccessCode> CreateAccessCode(string visitoruid, int? queueId);

        /// <summary>
        /// DeleteAccessCodeById
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<string> DeleteAccessCodeByCode(string code);

        /// <summary>
        /// ModifyAccessCodeById
        /// </summary>
        /// <param name="code"></param>
        /// <param name="createdtime"></param>
        /// <param name="endtime"></param>
        /// <param name="status"></param>
        /// <param name="visitoruid"></param>
        /// <param name="queueId"></param>
        /// <returns></returns>
        Task<AccessCode> ModifyAccessCodeByCode(string code, string createdtime, string endtime, string status, string visitoruid, int? queueId);
    }
}