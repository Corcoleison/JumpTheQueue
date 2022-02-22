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
    /// Repository implementation for the Queue
    /// </summary>
    public class QueueRepository : Repository<Queue>, IQueueRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public QueueRepository(jumpthequeueContext context) : base(context)
        {
        }

        /// <summary>
        /// Get TODO method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<IList<Queue>> GetQueue(Expression<Func<Queue, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetTodo method from TodoRepository Queueervice");
            return Get(predicate);
        }

        /// <summary>
        /// Gets the Queues by clientId
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public Task<IList<Queue>> GetQueueByClientId(string clientid, Expression<Func<Queue, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetQueueByClientId method from TodoRepository Queueervice");
            return Get(predicate);
        }

        /// <summary>
        /// Gets the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Queue> GetQueueById(int id)
        {
            Devon4NetLogger.Debug($"GetTodoById method from repository Queueervice with value : {id}");
            return GetFirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Creates the TODO
        /// </summary>
        /// <param name="name"></param>
        /// <param name="logo"></param>
        /// <param name="accesslink"></param>
        /// <param name="minattentiontime"></param>
        /// <param name="opentime"></param>
        /// <param name="closetime"></param>
        /// <param name="started"></param>ç
        /// <param name="closed"></param>
        /// <param name="userclientid"></param>
        /// <returns></returns>
        public Task<Queue> Create(string name, string logo, string accesslink, int? minattentiontime, string opentime, string closetime, bool? started, bool? closed, string userclientid)
        {
            Devon4NetLogger.Debug($"Create method from repository Queueervice with value : {name}");
            return Create(new Queue{Name=name, Logo=logo, Accesslink=accesslink, Minattentiontime=minattentiontime, Opentime=opentime, Closetime=closetime, Started=started, Closed=closed, UserClientid= userclientid });
        }

        /// <summary>
        /// Deletes the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteQueueById(int id)
        {
            Devon4NetLogger.Debug($"DeleteTodoById method from repository Queueervice with value : {id}");
            var deleted = await Delete(t => t.Id == id).ConfigureAwait(false);

            if (deleted)
            {
                return id;
            }

            throw  new ApplicationException($"The Queue entity {id} has not been deleted.");
        }
    }
}
