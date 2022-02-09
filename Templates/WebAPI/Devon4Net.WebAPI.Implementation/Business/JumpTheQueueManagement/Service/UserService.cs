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
    /// User service implementation
    /// </summary>
    public class UserService: Service<jumpthequeueContext>, IUserService
    {
        private readonly IUserRepository _UserRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uoW"></param>
        public UserService(IUnitOfWork<jumpthequeueContext> uoW) : base(uoW)
        {
            _UserRepository = uoW.Repository<IUserRepository>();
        }

        /// <summary>
        /// Gets the User
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserDto>> GetUser(Expression<Func<User, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetUser method from service Userervice");
            var result = await _UserRepository.GetUser(predicate).ConfigureAwait(false);
            return result.Select(UserConverter.ModelToDto);
        }

        /// <summary>
        /// Gets the User by id
        /// </summary>
        /// <param name="Clientid"></param>
        /// <returns></returns>
        public Task<User> GetUserByClientid(string Clientid)
        {
            Devon4NetLogger.Debug($"GetUserById method from service Userervice with value : {Clientid}");
            return _UserRepository.GetUserByClientid(Clientid);
        }

        /// <summary>
        /// Creates the User
        /// </summary>
        /// <param name="Clientid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<User> CreateUser(string Clientid, string role)
        {
            Devon4NetLogger.Debug($"SetUser method from service Userervice with value : {Clientid}");

            if (string.IsNullOrEmpty(Clientid) || string.IsNullOrWhiteSpace(role.ToString()))
            {
                throw new ArgumentException("The 'Clientid' field can not be null.");
            }
            Role_t role_parsed = (Role_t)Enum.Parse(typeof(Role_t), role, true);

            return _UserRepository.Create(Clientid, role_parsed);
        }

        /// <summary>
        /// Deletes the User by id
        /// </summary>
        /// <param name="Clientid"></param>
        /// <returns></returns>
        public async Task<string> DeleteUserByClientid(string Clientid)
        {
            Devon4NetLogger.Debug($"DeleteUserById method from service Userervice with value : {Clientid}");
            var User = await _UserRepository.GetFirstOrDefault(t => t.Clientid == Clientid).ConfigureAwait(false);

            if (User == null)
            {
                throw new ArgumentException($"The provided Id {Clientid} does not exists");
            }

            return await _UserRepository.DeleteUserByClientid(Clientid).ConfigureAwait(false);
        }

        /// <summary>
        /// Modifies te state of the User by id
        /// </summary>
        /// <param name="Clientid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<User> ModifyUserByClientid(string Clientid, string role)
        {
            Devon4NetLogger.Debug($"ModifyUserById method from service Userervice with value : {Clientid}");
            var User = await _UserRepository.GetFirstOrDefault(t => t.Clientid == Clientid).ConfigureAwait(false);

            if (User == null)
            {
                throw new UserNotFoundException($"The User with id {Clientid} does not exists and is not possible to modify.");
            }

            Role_t role_parsed = (Role_t)Enum.Parse(typeof(Role_t), role, true);

            User.Clientid= Clientid;
            User.Role = role_parsed;

            return await _UserRepository.Update(User).ConfigureAwait(false);
        }
    }
}