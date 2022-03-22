using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Service
{
    /// <summary>
    /// IQueueService
    /// </summary>
    public interface IQueueService
    {
        /// <summary>
        /// GetQueue
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<QueueDto>> GetQueue(Expression<Func<Queue, bool>> predicate = null);

        /// <summary>
        /// GetQueueById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Queue> GetQueueById(int id);

        /// <summary>
        /// CreateQueue
        /// </summary>
        /// <param name="name"></param>
        /// <param name="logo"></param>
        /// <param name="accesslink"></param>
        /// <param name="minattentiontime"></param>
        /// <param name="opentime"></param>
        /// <param name="closetime"></param>
        /// <param name="started"></param>ç
        /// <param name="closed"></param>
        /// <returns></returns>
        Task<Queue> CreateQueue(string name, string logo, string accesslink, int? minattentiontime, string opentime, string closetime, bool? started, bool? closed, string userclientid);

        /// <summary>
        /// DeleteQueueById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteQueueById(int id);

        /// <summary>
        /// ModifyQueueById
        /// </summary>
        /// <param name="id"></param>
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
        Task<Queue> ModifyQueueById(int id,string name, string logo, string accesslink, int? minattentiontime, string opentime, string closetime, bool? started, bool? closed, string userclientid);

        /// <summary>
        /// Show the attended ticket
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<string> NextAttendedTicketByName(string name);
    }
}