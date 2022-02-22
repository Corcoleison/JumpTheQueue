using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// AccessCodeRepository interface
    /// </summary>
    public interface IAccessCodeRepository : IRepository<AccessCode>
    {
        /// <summary>
        /// GetAccessCode
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<AccessCode>> GetAccessCode(Expression<Func<AccessCode, bool>> predicate = null);

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
        Task<AccessCode> GetAccessCodeByVisitorAndQueue(string visitoruid, int? queueid);

        /// <summary>
        /// GetAccessCodeByQueue
        /// </summary>
        /// <param name="queueid"></param>
        /// <returns></returns>
        Task<AccessCode> GetLastAccessCodeByQueue(int? queueid);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="code"></param>
        /// <param name="createdtime"></param>
        /// <param name="endtime"></param>
        /// <param name="status"></param>
        /// <param name="visitoruid"></param>
        /// <param name="queueId"></param>
        /// <returns></returns>
        Task<AccessCode> Create(string code, TimeSpan? createdtime, TimeSpan? endtime, Status_t status, string visitoruid, int? queueId);

        /// <summary>
        /// DeleteAccessCodeById
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<string> DeleteAccessCodeByCode(string code);
    }
}
