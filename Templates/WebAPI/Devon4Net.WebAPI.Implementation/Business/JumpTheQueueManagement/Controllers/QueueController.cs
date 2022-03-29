using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Hubs;
using Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Devon4Net.WebAPI.Implementation.Business.JumpTheQueueManagement.Controllers
{
    /// <summary>
    /// Queues controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _QueueService;
        private readonly IHubContext<ColaHub> _hubContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="QueueService"></param>
        public QueueController(IQueueService QueueService, IHubContext<ColaHub> hubContext)
        {
            _QueueService = QueueService;
            _hubContext = hubContext;
        }

        
        [HttpPost("{id}/next")]
        public ActionResult Test1(string id)
        {
            _hubContext.Clients.Group(id).SendAsync("receiveNext", "Q001");
            return Ok();
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

        /// <summary>
        /// Gets the attendedTicket of a queue
        /// </summary>
        /// <returns></returns>
        [HttpGet("{QueueName}/next")]
        public async Task<ActionResult> NextAttendedTicket(string QueueName)
        {
            Devon4NetLogger.Debug("Executing GetAttendedTicket from controller QueueController");
            var attendedTicket = await _QueueService.NextAttendedTicketByName(QueueName).ConfigureAwait(false);
            await _hubContext.Clients.Group("queue1").SendAsync("receiveNext", attendedTicket).ConfigureAwait(false);
            return Ok(attendedTicket);
        }

        /// <summary>
        /// Start a queue
        /// </summary>
        /// <returns></returns>
        [HttpGet("{QueueName}/start")]
        public async Task<ActionResult> StartQueue(string QueueName)
        {
            Devon4NetLogger.Debug("Executing GetAttendedTicket from controller QueueController");
            var accesCodes = await _QueueService.StartQueue(QueueName).ConfigureAwait(false);
            if (accesCodes == null)
            {
                return BadRequest("Unable to start Queue");
            }
            //await _hubContext.Clients.Group(QueueName).SendAsync("startQueue", QueueName).ConfigureAwait(false);
            return Ok(accesCodes);
        }


        /// <summary>
        /// Get all the access codes by queueid
        /// </summary>
        /// <param name="Queueid"></param>
        /// <returns></returns>
        [HttpGet("{Queueid}/getAllAccessCodes")]
        public async Task<ActionResult> GetAllAccessCodeByQueueId(int Queueid)
        {
            Devon4NetLogger.Debug("Executing GetAttendedTicket from controller QueueController");
            return Ok(await _QueueService.GetAllAccessCodeByQueueId(Queueid).ConfigureAwait(false));
        }
    }
}
