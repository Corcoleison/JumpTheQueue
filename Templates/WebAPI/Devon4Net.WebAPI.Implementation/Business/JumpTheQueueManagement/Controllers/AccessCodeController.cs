using System;
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
    /// AccessCodes controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class AccessCodeController: ControllerBase
    {
        private readonly IAccessCodeService _AccessCodeService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="AccessCodeService"></param>
        public AccessCodeController( IAccessCodeService AccessCodeService)
        {
            _AccessCodeService = AccessCodeService;
        }


        /// <summary>
        /// Gets the entire list of AccessCodes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<AccessCodeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAccessCode()
        {
            Devon4NetLogger.Debug("Executing GetAccessCode from controller AccessCodeController");
            return Ok(await _AccessCodeService.GetAccessCode().ConfigureAwait(false));
        }


        /// <summary>
        /// Creates an AccessCode
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccessCodeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([Required] int queueid)
        {
            Devon4NetLogger.Debug("Executing GetAccessCode from controller AccessCodeController");
            var result = await _AccessCodeService.CreateAccessCode(queueid).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Deletes the AccessCode provided the id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(AccessCodeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required]string AccessCode)
        {
            Devon4NetLogger.Debug("Executing GetAccessCode from controller AccessCodeController");
            return Ok(await _AccessCodeService.DeleteAccessCodeByCode(AccessCode).ConfigureAwait(false));
        }

        /// <summary>
        /// Modifies the done status of the AccessCode provided the data of the AccessCode
        /// In this sample, all the data fields are mandatory
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(AccessCodeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ModifyAccessCode(AccessCodeDto AccessCodeDto)
        {
            Devon4NetLogger.Debug("Executing ModifyAccessCode from controller AccessCodeController");
            if (string.IsNullOrEmpty(AccessCodeDto.Code))
            {
                return BadRequest("The id of the AccessCode must be provided");
            }
            return Ok(await _AccessCodeService.ModifyAccessCodeByCode(AccessCodeDto.Code, AccessCodeDto.Createdtime, AccessCodeDto.Endtime, AccessCodeDto.Status, AccessCodeDto.VisitorUid, AccessCodeDto.QueueId).ConfigureAwait(false));
        }
    }
}
