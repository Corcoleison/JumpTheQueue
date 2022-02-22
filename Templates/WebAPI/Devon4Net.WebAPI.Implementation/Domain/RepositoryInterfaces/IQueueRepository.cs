using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// QueueRepository interface
    /// </summary>
    public interface IQueueRepository : IRepository<Queue>
    {
        /// <summary>
        /// GetQueue
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<Queue>> GetQueue(Expression<Func<Queue, bool>> predicate = null);

        /// <summary>
        /// GetQueueById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Queue> GetQueueById(int id);

        /// <summary>
        /// Create
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
        Task<Queue> Create(string name, string logo, string accesslink, int? minattentiontime, string opentime, string closetime, bool? started, bool? closed, string userclientid);

        /// <summary>
        /// DeleteQueueById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteQueueById(int id);
    }
}
