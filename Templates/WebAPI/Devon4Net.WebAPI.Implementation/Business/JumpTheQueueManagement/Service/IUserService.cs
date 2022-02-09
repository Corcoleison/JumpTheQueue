using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Service
{
    /// <summary>
    /// IUserService
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDto>> GetUser(Expression<Func<User, bool>> predicate = null);

        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="Clientid"></param>
        /// <returns></returns>
        Task<User> GetUserByClientid(string Clientid);

        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="Clientid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<User> CreateUser(string Clientid, Role_t role);

        /// <summary>
        /// DeleteUserById
        /// </summary>
        /// <param name="Clientid"></param>
        /// <returns></returns>
        Task<string> DeleteUserByClientid(string Clientid);

        /// <summary>
        /// ModifyUserById
        /// </summary>
        /// <param name="Clientid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<User> ModifyUserByClientid(string Clientid, Role_t role);
    }
}