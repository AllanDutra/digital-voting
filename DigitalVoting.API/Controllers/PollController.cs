using DigitalVoting.Application.Commands.CreateNewPoll;
using DigitalVoting.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalVoting.API.Controllers
{
    [ApiController]
    [Route("api/poll")]
    [Authorize]
    public class PollController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PollController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new poll with yours voting options
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateNewPollResponse), 200)]
        [ProducesResponseType(typeof(DefaultResponse), 400)]
        [ProducesResponseType(typeof(void), 401)]
        public async Task<IActionResult> CreateNewPollAsync([FromBody] CreateNewPollCommand command)
        {
            var newPollId = await _mediator.Send(command);

            return Ok(new CreateNewPollResponse("A new poll has been created!", newPollId));
        }
    }
}