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
    /// Repository implementation for the Visitor
    /// </summary>
    public class VisitorRepository : Repository<Visitor>, IVisitorRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public VisitorRepository(jumpthequeueContext context) : base(context)
        {
        }

        /// <summary>
        /// Get TODO method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<IList<Visitor>> GetVisitor(Expression<Func<Visitor, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetTodo method from TodoRepository Visitorervice");
            return Get(predicate);
        }

        /// <summary>
        /// Gets the TODO by id
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Task<Visitor> GetVisitorByUid(Guid uid)
        {
            Devon4NetLogger.Debug($"GetTodoById method from repository Visitorervice with value : {uid}");
            return GetFirstOrDefault(t => t.Uid == uid);
        }

        /// <summary>
        /// Creates the TODO
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Task<Visitor> Create(Guid uid)
        {
            Devon4NetLogger.Debug($"SetTodo method from repository Visitorervice with value : {uid}");
            return Create(new Visitor{ Uid = uid});
        }

        /// <summary>
        /// Deletes the TODO by id
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteVisitorByUid(Guid uid)
        {
            Devon4NetLogger.Debug($"DeleteTodoById method from repository Visitorervice with value : {uid}");
            var deleted = await Delete(t => t.Uid == uid).ConfigureAwait(false);

            if (deleted)
            {
                return uid;
            }

            throw  new ApplicationException($"The Todo entity {uid} has not been deleted.");
        }
    }
}
