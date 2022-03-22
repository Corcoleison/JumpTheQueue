using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Exceptions;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Service
{
    /// <summary>
    /// Queue service implementation
    /// </summary>
    public class QueueService: Service<jumpthequeueContext>, IQueueService
    {
        private readonly IQueueRepository _QueueRepository;
        private readonly IAccessCodeRepository _AccessCodeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uoW"></param>
        public QueueService(IUnitOfWork<jumpthequeueContext> uoW) : base(uoW)
        {
            _QueueRepository = uoW.Repository<IQueueRepository>();
            _AccessCodeRepository = uoW.Repository<IAccessCodeRepository>();
        }

        /// <summary>
        /// Gets the Queue
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<QueueDto>> GetQueue(Expression<Func<Queue, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetQueue method from service Queueervice");
            var result = await _QueueRepository.GetQueue(predicate).ConfigureAwait(false);
            return result.Select(QueueConverter.ModelToDto);
        }

        /// <summary>
        /// Gets the Queue by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Queue> GetQueueById(int id)
        {
            Devon4NetLogger.Debug($"GetQueueById method from service Queueervice with value : {id}");
            return _QueueRepository.GetQueueById(id);
        }

        /// <summary>
        /// Creates the Queue
        /// </summary>
        /// <param name="name"></param>
        /// <param name="logo"></param>
        /// <param name="accesslink"></param>
        /// <param name="minattentiontime"></param>
        /// <param name="opentime"></param>
        /// <param name="closetime"></param>
        /// <param name="started"></param>
        /// <param name="closed"></param>
        /// <param name="userclientid"></param>
        /// <returns></returns>
        public Task<Queue> CreateQueue(string name, string logo, string accesslink, int? minattentiontime, string opentime, string closetime, bool? started, bool? closed, string userclientid)
        {
            Devon4NetLogger.Debug($"SetQueue method from service Queueervice with value : {name}");

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The 'Clientid' field can not be null.");
            }

            return _QueueRepository.Create(name, logo, accesslink, minattentiontime, opentime, closetime, started, closed, userclientid);
        }

        /// <summary>
        /// Deletes the Queue by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteQueueById(int id)
        {
            Devon4NetLogger.Debug($"DeleteQueueById method from service Queueervice with value : {id}");
            var Queue = await _QueueRepository.GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);

            if (Queue == null)
            {
                throw new ArgumentException($"The provided Id {id} does not exists");
            }

            return await _QueueRepository.DeleteQueueById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Modifies te state of the Queue by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="logo"></param>
        /// <param name="accesslink"></param>
        /// <param name="minattentiontime"></param>
        /// <param name="opentime"></param>
        /// <param name="closetime"></param>
        /// <param name="started"></param>
        /// <param name="closed"></param>
        /// <param name="userclientid"></param>
        /// <returns></returns>
        public async Task<Queue> ModifyQueueById(int id, string name, string logo, string accesslink, int? minattentiontime, string opentime, string closetime, bool? started, bool? closed, string userclientid)
        {
            Devon4NetLogger.Debug($"ModifyQueueById method from service Queueervice with value : {id}");
            var Queue = await _QueueRepository.GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);

            if (Queue == null)
            {
                throw new QueueNotFoundException($"The Queue with id {name} does not exists and is not possible to modify.");
            }

            Queue.Name= name;
            Queue.Logo = logo;
            Queue.Accesslink = accesslink;
            Queue.Minattentiontime = minattentiontime;
            Queue.Opentime = opentime;
            Queue.Closetime = closetime;
            Queue.Started = started;
            Queue.Closed = closed;
            Queue.UserClientid = userclientid;

            return await _QueueRepository.Update(Queue).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<string> NextAttendedTicketByName(string name)
        {
            var cola = await _QueueRepository.GetFirstOrDefault(t => t.Name == name).ConfigureAwait(false);
            AccessCode attendedAccessCode = null;
            var accessCodeList = await _AccessCodeRepository.GetAccessCode(t => t.Id == cola.Id).ConfigureAwait(false);
            foreach(var accessC in accessCodeList)
            {
                if(accessC.Endtime == null && accessC.Createdtime != null && accessC.Status != Status_t.attended)
                {
                    accessC.Status = Status_t.attended;
                    accessC.Endtime = DateTime.Now.TimeOfDay;
                    await _AccessCodeRepository.Update(accessC).ConfigureAwait(false);
                    attendedAccessCode = accessC;
                    break;
                }
            }
            return attendedAccessCode.Code;

        }
    }
}