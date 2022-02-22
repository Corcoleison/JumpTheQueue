using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the AccessCode
    /// </summary>
    public class AccessCodeRepository : Repository<AccessCode>, IAccessCodeRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public AccessCodeRepository(jumpthequeueContext context) : base(context)
        {
        }

        /// <summary>
        /// Get TODO method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<IList<AccessCode>> GetAccessCode(Expression<Func<AccessCode, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetTodo method from TodoRepository AccessCodeervice");
            return Get(predicate);
        }

        /// <summary>
        /// Gets the TODO by id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task<AccessCode> GetAccessCodeByCode(string code)
        {
            Devon4NetLogger.Debug($"GetTodoById method from repository AccessCodeervice with value : {code}");
            return GetFirstOrDefault(t => t.Code == code);
        }

        /// <summary>
        /// Gets the AccessCode by queue id
        /// </summary>
        /// <param name="queueid"></param>
        /// <param name="visitoruid"></param>
        public Task<AccessCode> GetAccessCodeByVisitorAndQueue(Guid visitoruid, int queueid)
        {
            Devon4NetLogger.Debug($"GetTodoById method from repository AccessCodeervice with value : {visitoruid} and {queueid}");
            return GetFirstOrDefault(t => t.VisitorUid == visitoruid && t.QueueId == queueid);
        }

        /// <summary>
        /// Gets the TODO by id
        /// </summary>
        /// <param name="queueid"></param>
        /// <returns></returns>
        public Task<AccessCode> GetLastAccessCodeByQueue(int queueid)
        {
            Devon4NetLogger.Debug($"GetTodoById method from repository AccessCodeervice with value : {queueid}");
            return GetLastOrDefault(t => t.QueueId == queueid);
        }

        /// <summary>
        /// Creates the TODO
        /// </summary>
        /// <param name="code"></param>
        /// <param name="createdtime"></param>
        /// <param name="endtime"></param>
        /// <param name="status"></param>
        /// <param name="visitoruid"></param>
        /// <param name="queueId"></param>
        /// <returns></returns>
        public Task<AccessCode> Create(string code, TimeSpan? createdtime, TimeSpan? endtime, Status_t status, Guid visitoruid, int queueId)
        {
            Devon4NetLogger.Debug($"SetTodo method from repository AccessCodeervice with value : {code}");
            return Create(new AccessCode{ Code= code, Createdtime=createdtime, Endtime= endtime, Status= status, VisitorUid= visitoruid, QueueId= queueId});
        }

        /// <summary>
        /// Deletes the TODO by id
        /// </summary>
        /// <param name="code"></param>
        /// <returnwos></returns>
        public async Task<string> DeleteAccessCodeByCode(string code)
        {
            Devon4NetLogger.Debug($"DeleteTodoById method from repository AccessCodeervice with value : {code}");
            var deleted = await Delete(t => t.Code == code).ConfigureAwait(false);

            if (deleted)
            {
                return code;
            }

            throw  new ApplicationException($"The AccessCode entity {code} has not been deleted.");
        }
    }
}
