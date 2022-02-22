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
    /// Queues controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class QueueController: ControllerBase
    {
        private readonly IQueueService _QueueService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="QueueService"></param>
        public QueueController( IQueueService QueueService)
        {
            _QueueService = QueueService;
        }


        /// <summary>
        /// Gets the entire list of Queues
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<QueueDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetQueue()
        {
            Devon4NetLogger.Debug("Executing GetQueue from controller QueueController");
            return Ok(await _QueueService.GetQueue().ConfigureAwait(false));
        }

        /// <summary>
        /// Creates an Queue
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(QueueDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(QueueDto QueueDto)
        {
            Devon4NetLogger.Debug("Executing GetQueue from controller QueueController");
            var result = await _QueueService.CreateQueue(QueueDto.Name, QueueDto.Logo, QueueDto.Accesslink, QueueDto.Minattentiontime, QueueDto.Opentime, QueueDto.Closetime, QueueDto.Started, QueueDto.Closed, QueueDto.UserClientid).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Deletes the Queue provided the id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(QueueDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required]int id)
        {
            Devon4NetLogger.Debug("Executing GetQueue from controller QueueController");
            return Ok(await _QueueService.DeleteQueueById(id).ConfigureAwait(false));
        }

        /// <summary>
        /// Modifies the done status of the Queue provided the data of the Queue
        /// In this sample, all the data fields are mandatory
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(QueueDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ModifyQueue(QueueDto QueueDto)
        {
            Devon4NetLogger.Debug("Executing ModifyQueue from controller QueueController");
            if (QueueDto == null || QueueDto.Id == 0)
            {
                return BadRequest("The id of the Queue must be provided");
            }
            return Ok(await _QueueService.ModifyQueueById(QueueDto.Id, QueueDto.Name, QueueDto.Logo, QueueDto.Accesslink, QueueDto.Minattentiontime, QueueDto.Opentime, QueueDto.Closetime, QueueDto.Started, QueueDto.Closed, QueueDto.UserClientid).ConfigureAwait(false));
        }
    }
}
