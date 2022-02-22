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
    /// Visitor service implementation
    /// </summary>
    public class VisitorService: Service<jumpthequeueContext>, IVisitorService
    {
        private readonly IVisitorRepository _VisitorRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uoW"></param>
        public VisitorService(IUnitOfWork<jumpthequeueContext> uoW) : base(uoW)
        {
            _VisitorRepository = uoW.Repository<IVisitorRepository>();
        }

        /// <summary>
        /// Gets the Visitor
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VisitorDto>> GetVisitor(Expression<Func<Visitor, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetVisitor method from service Visitorervice");
            var result = await _VisitorRepository.GetVisitor(predicate).ConfigureAwait(false);
            return result.Select(VisitorConverter.ModelToDto);
        }

        /// <summary>
        /// Gets the Visitor by id
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Task<Visitor> GetVisitorByUid(Guid uid)
        {
            Devon4NetLogger.Debug($"GetVisitorById method from service Visitorervice with value : {uid}");
            return _VisitorRepository.GetVisitorByUid(uid);
        }

        /// <summary>
        /// Creates the Visitor
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Task<Visitor> CreateVisitor(Guid uid)
        {
            Devon4NetLogger.Debug($"SetVisitor method from service Visitorervice with value : {uid}");

            if (uid == null || uid == Guid.Empty)
            {
                throw new ArgumentException("The 'uid' field can not be null.");
            }

            return _VisitorRepository.Create(uid);
        }
        
        /// <summary>
        /// Deletes the Visitor by id
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteVisitorByUid(Guid uid)
        {
            Devon4NetLogger.Debug($"DeleteVisitorById method from service Visitorervice with value : {uid}");
            var Visitor = await _VisitorRepository.GetFirstOrDefault(t => t.Uid == uid).ConfigureAwait(false);

            if (Visitor == null)
            {
                throw new ArgumentException($"The provided Id {uid} does not exists");
            }

            return await _VisitorRepository.DeleteVisitorByUid(uid).ConfigureAwait(false);
        }

        /// <summary>
        /// Modifies te state of the Visitor by id
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<Visitor> ModifyVisitorByUid(Guid uid)
        {
            Devon4NetLogger.Debug($"ModifyVisitorById method from service Visitorervice with value : {uid}");
            var Visitor = await _VisitorRepository.GetFirstOrDefault(t => t.Uid == uid).ConfigureAwait(false);

            if (Visitor == null)
            {
                throw new VisitorNotFoundException($"The Visitor with id {uid} does not exists and is not possible to modify.");
            }

            Visitor.Uid= uid;

            return await _VisitorRepository.Update(Visitor).ConfigureAwait(false);
        }
    }
}