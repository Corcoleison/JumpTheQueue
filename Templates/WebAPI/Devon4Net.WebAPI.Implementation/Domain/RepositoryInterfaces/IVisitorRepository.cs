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
        /// <param name="Uid"></param>
        /// <returns></returns>
        Task<Visitor> GetVisitorByUid(string uid);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        Task<Visitor> Create(string uid);

        /// <summary>
        /// DeleteVisitorById
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        Task<string> DeleteVisitorByUid(string uid);
    }
}
