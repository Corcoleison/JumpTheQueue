using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// VisitorRepository interface
    /// </summary>
    public interface IVisitorRepository : IRepository<Visitor>
    {
        /// <summary>
        /// GetVisitor
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<Visitor>> GetVisitor(Expression<Func<Visitor, bool>> predicate = null);

        /// <summary>
        /// GetVisitorById
        /// </summary>
        /// <param uid="uid"></param>
        /// <returns></returns>
        Task<Visitor> GetVisitorByUid(Guid uid);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<Visitor> Create(Guid uid);

        /// <summary>
        /// DeleteVisitorById
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<Guid> DeleteVisitorByUid(Guid uid);
    }
}
