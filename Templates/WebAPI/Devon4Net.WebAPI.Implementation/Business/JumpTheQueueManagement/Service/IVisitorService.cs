using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Service
{
    /// <summary>
    /// IVisitorService
    /// </summary>
    public interface IVisitorService
    {
        /// <summary>
        /// GetVisitor
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<VisitorDto>> GetVisitor(Expression<Func<Visitor, bool>> predicate = null);

        /// <summary>
        /// GetVisitorById
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<Visitor> GetVisitorByUid(Guid uid);

        /// <summary>
        /// CreateVisitor
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<Visitor> CreateVisitor(Guid uid);

        /// <summary>
        /// DeleteVisitorById
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<Guid> DeleteVisitorByUid(Guid uid);

        /// <summary>
        /// ModifyVisitorById
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<Visitor> ModifyVisitorByUid(Guid uid);
    }
}