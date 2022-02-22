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
    /// Visitors controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class VisitorController: ControllerBase
    {
        private readonly IVisitorService _VisitorService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="VisitorService"></param>
        public VisitorController( IVisitorService VisitorService)
        {
            _VisitorService = VisitorService;
        }


        /// <summary>
        /// Gets the entire list of Visitors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<VisitorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetVisitor()
        {
            Devon4NetLogger.Debug("Executing GetVisitor from controller VisitorController");
            return Ok(await _VisitorService.GetVisitor().ConfigureAwait(false));
        }

        /// <summary>
        /// Creates an Visitor
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(VisitorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(VisitorDto VisitorDto)
        {
            Devon4NetLogger.Debug("Executing GetVisitor from controller VisitorController");
            var result = await _VisitorService.CreateVisitor(VisitorDto.Uid).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Deletes the Visitor provided the id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(VisitorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required]Guid VisitorUid)
        {
            Devon4NetLogger.Debug("Executing GetVisitor from controller VisitorController");
            return Ok(await _VisitorService.DeleteVisitorByUid(VisitorUid).ConfigureAwait(false));
        }

        /// <summary>
        /// Modifies the done status of the Visitor provided the data of the Visitor
        /// In this sample, all the data fields are mandatory
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(VisitorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ModifyVisitor(VisitorDto VisitorDto)
        {
            Devon4NetLogger.Debug("Executing ModifyVisitor from controller VisitorController");
            if (VisitorDto.Uid == null || VisitorDto.Uid == Guid.Empty)
            {
                return BadRequest("The id of the Visitor must be provided");
            }
            return Ok(await _VisitorService.ModifyVisitorByUid(VisitorDto.Uid).ConfigureAwait(false));
        }
    }
}
