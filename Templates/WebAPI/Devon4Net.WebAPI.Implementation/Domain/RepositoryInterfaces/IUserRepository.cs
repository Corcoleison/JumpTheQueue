using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// UserRepository interface
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<User>> GetUser(Expression<Func<User, bool>> predicate = null);

        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="Clientid"></param>
        /// <returns></returns>
        Task<User> GetUserByClientid(string Clientid);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Clientid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<User> Create(string Clientid, Role_t role);

        /// <summary>
        /// DeleteUserById
        /// </summary>
        /// <param name="Clientid"></param>
        /// <returns></returns>
        Task<string> DeleteUserByClientid(string Clientid);
    }
}
