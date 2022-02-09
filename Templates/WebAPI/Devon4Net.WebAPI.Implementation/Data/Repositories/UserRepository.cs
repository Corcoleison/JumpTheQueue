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
    /// Repository implementation for the User
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(jumpthequeueContext context) : base(context)
        {
        }

        /// <summary>
        /// Get TODO method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<IList<User>> GetUser(Expression<Func<User, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetTodo method from TodoRepository Userervice");
            return Get(predicate);
        }

        /// <summary>
        /// Gets the TODO by id
        /// </summary>
        /// <param name="Clientid"></param>
        /// <returns></returns>
        public Task<User> GetUserByClientid(string Clientid)
        {
            Devon4NetLogger.Debug($"GetTodoById method from repository Userervice with value : {Clientid}");
            return GetFirstOrDefault(t => t.Clientid == Clientid);
        }

        /// <summary>
        /// Creates the TODO
        /// </summary>
        /// <param name="Clientid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<User> Create(string Clientid, Role_t role)
        {
            Devon4NetLogger.Debug($"SetTodo method from repository Userervice with value : {Clientid}");
            return Create(new User{ Clientid = Clientid, Role=role});
        }

        /// <summary>
        /// Deletes the TODO by id
        /// </summary>
        /// <param name="Clientid"></param>
        /// <returns></returns>
        public async Task<string> DeleteUserByClientid(string Clientid)
        {
            Devon4NetLogger.Debug($"DeleteTodoById method from repository Userervice with value : {Clientid}");
            var deleted = await Delete(t => t.Clientid == Clientid).ConfigureAwait(false);

            if (deleted)
            {
                return Clientid;
            }

            throw  new ApplicationException($"The User entity {Clientid} has not been deleted.");
        }
    }
}
