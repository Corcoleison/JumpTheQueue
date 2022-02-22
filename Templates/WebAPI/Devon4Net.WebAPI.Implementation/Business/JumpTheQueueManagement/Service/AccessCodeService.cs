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
    /// AccessCode service implementation
    /// </summary>
    public class AccessCodeService: Service<jumpthequeueContext>, IAccessCodeService
    {
        private readonly IAccessCodeRepository _AccessCodeRepository;
        private readonly IVisitorRepository _VisitorRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uoW"></param>
        public AccessCodeService(IUnitOfWork<jumpthequeueContext> uoW) : base(uoW)
        {
            _AccessCodeRepository = uoW.Repository<IAccessCodeRepository>();
            _VisitorRepository = uoW.Repository<IVisitorRepository>();
        }

        /// <summary>
        /// Gets the AccessCode
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AccessCodeDto>> GetAccessCode(Expression<Func<AccessCode, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetAccessCode method from service AccessCodeervice");
            var result = await _AccessCodeRepository.GetAccessCode(predicate).ConfigureAwait(false);
            return result.Select(AccessCodeConverter.ModelToDto);
        }

        /// <summary>
        /// Gets the AccessCode by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task<AccessCode> GetAccessCodeByCode(string code)
        {
            Devon4NetLogger.Debug($"GetAccessCodeById method from service AccessCodeervice with value : {code}");
            return _AccessCodeRepository.GetAccessCodeByCode(code);
        }

        /// <summary>
        /// Gets the AccessCode by queue id
        /// </summary>
        /// <param name="queueid"></param>
        /// <param name="visitoruid"></param>
        public Task<AccessCode> GetAccessCodeByVisitorAndQueue(Guid visitoruid, int queueid)
        {
            Devon4NetLogger.Debug($"GetAccessCodeById method from service AccessCodeervice with value : {visitoruid} and {queueid}");
            return _AccessCodeRepository.GetAccessCodeByVisitorAndQueue(visitoruid,queueid);
        }

        /// <summary>
        /// Gets the AccessCode by queue id
        /// </summary>
        /// <param name="queueid"></param>
        /// <returns></returns>
        public async Task<AccessCode> GetLastAccessCodeByQueue(int queueid)
        {
            Devon4NetLogger.Debug($"GetAccessCodeById method from service AccessCodeervice with value : {queueid}");
            var list= await _AccessCodeRepository.GetAccessCode(t => t.QueueId == queueid).ConfigureAwait(false);
            var lastAC = list.OrderByDescending(x => x.Code).ToList().FirstOrDefault();
            return lastAC;
        }

        /// <summary>
        /// Creates the AccessCode
        /// </summary>
        /// <param name="visitoruid"></param>
        /// <param name="queueId"></param>
        /// <returns></returns>
        public async Task<AccessCode> CreateAccessCode(int queueId)
        {
            Guid visitoruid = Guid.NewGuid();
            await _VisitorRepository.Create(visitoruid).ConfigureAwait(false);
            Devon4NetLogger.Debug($"SetAccessCode method from service AccessCodeervice with values : {visitoruid} and {queueId}");
            string code = await ChooseCodeAsync(visitoruid, queueId).ConfigureAwait(false);
            Status_t statusCreated = Status_t.notStarted;
            if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(statusCreated.ToString()))
            {
                throw new ArgumentException("The 'Clientid' field can not be null.");
            }
            //Status_t status_parsed = (Status_t)Enum.Parse(typeof(Status_t), status, true);
            TimeSpan createdtime = DateTime.Now.TimeOfDay;
            TimeSpan? endtime_parsed = null;

            return await _AccessCodeRepository.Create(code, createdtime, endtime_parsed, statusCreated, visitoruid, queueId).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the AccessCode by id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<string> DeleteAccessCodeByCode(string code)
        {
            Devon4NetLogger.Debug($"DeleteAccessCodeByCode method from service AccessCodeervice with value : {code}");
            var AccessCode = await _AccessCodeRepository.GetFirstOrDefault(t => t.Code == code).ConfigureAwait(false);

            if (AccessCode == null)
            {
                throw new ArgumentException($"The provided Code {code} does not exists");
            }
            var result = await _AccessCodeRepository.DeleteAccessCodeByCode(code).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Modifies te state of the AccessCode by id
        /// </summary>
        /// <param name="code"></param>
        /// <param name="createdtime"></param>
        /// <param name="endtime"></param>
        /// <param name="status"></param>
        /// <param name="visitoruid"></param>
        /// <param name="queueId"></param>
        /// <returns></returns>
        public async Task<AccessCode> ModifyAccessCodeByCode(string code, string createdtime, string endtime, string status, Guid visitoruid, int queueId)
        {
            Devon4NetLogger.Debug($"ModifyAccessCodeById method from service AccessCodeervice with value : {code}");
            var AccessCode = await _AccessCodeRepository.GetFirstOrDefault(t => t.Code == code).ConfigureAwait(false);

            if (AccessCode == null)
            {
                throw new AccessCodeNotFoundException($"The AccessCode with id {code} does not exists and is not possible to modify.");
            }

            Status_t status_parsed = (Status_t)Enum.Parse(typeof(Status_t), status, true);
            TimeSpan createdtime_parsed = TimeSpan.Parse(createdtime);
            TimeSpan endtime_parsed = TimeSpan.Parse(endtime);

            AccessCode.Code= code;
            AccessCode.Createdtime = createdtime_parsed;
            AccessCode.Endtime = endtime_parsed;
            AccessCode.Status = status_parsed;
            AccessCode.VisitorUid = visitoruid;
            AccessCode.QueueId = queueId;


            return await _AccessCodeRepository.Update(AccessCode).ConfigureAwait(false);
        }

        private async Task<string> ChooseCodeAsync(Guid visitoruid, int queueid)
        {
            Devon4NetLogger.Debug("Enters ChooseCodeAsync to choose next code");
            string resultCode = "Q001";
            var lastAC = await GetLastAccessCodeByQueue(queueid).ConfigureAwait(false);
            if(lastAC == null)
            {
                Devon4NetLogger.Debug($"Exit ChooseCodeAsync to choose next code {resultCode}");
                return resultCode;
            }
            else
            {
                string numberS = new String(lastAC.Code.Where(Char.IsDigit).ToArray());
                int number = Int32.Parse(numberS);
                number++;
                if (number > 999)
                {
                    number = 1;
                }
                resultCode = "Q"+number.ToString("000");
            }
            Devon4NetLogger.Debug($"Exit ChooseCodeAsync to choose next code {resultCode}");
            return resultCode;

        }
    }
}