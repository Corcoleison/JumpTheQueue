using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Controllers
{
    /// <summary>
    /// Users controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _UserService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="UserService"></param>
        public UserController( IUserService UserService)
        {
            _UserService = UserService;
        }


        /// <summary>
        /// Gets the entire list of Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUser()
        {
            Devon4NetLogger.Debug("Executing GetUser from controller UserController");
            return Ok(await _UserService.GetUser().ConfigureAwait(false));
        }

        /// <summary>
        /// Creates an User
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(UserDto UserDto)
        {
            Devon4NetLogger.Debug("Executing GetUser from controller UserController");
            var result = await _UserService.CreateUser(UserDto.Clientid, UserDto.Role).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Deletes the User provided the id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required]string UserClientid)
        {
            Devon4NetLogger.Debug("Executing GetUser from controller UserController");
            return Ok(await _UserService.DeleteUserByClientid(UserClientid).ConfigureAwait(false));
        }

        /// <summary>
        /// Modifies the done status of the User provided the data of the User
        /// In this sample, all the data fields are mandatory
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ModifyUser(UserDto UserDto)
        {
            Devon4NetLogger.Debug("Executing ModifyUser from controller UserController");
            if (string.IsNullOrEmpty(UserDto.Clientid))
            {
                return BadRequest("The id of the User must be provided");
            }
            return Ok(await _UserService.ModifyUserByClientid(UserDto.Clientid, UserDto.Role).ConfigureAwait(false));
        }
    }
}
