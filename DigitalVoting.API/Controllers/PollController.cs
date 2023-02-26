using DigitalVoting.Application.Commands.CreateNewPoll;
using DigitalVoting.Application.Commands.DeletePollOption;
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

        /// <summary>
        /// Delete a specific option by its id
        /// </summary>
        /// <param name="optionId">Id of the option you want to delete</param>
        /// <returns></returns>
        [HttpDelete("delete-option/{optionId}")]
        [ProducesResponseType(typeof(DefaultResponse), 200)]
        [ProducesResponseType(typeof(DefaultResponse), 400)]
        [ProducesResponseType(typeof(void), 401)]
        public async Task<IActionResult> DeletePollOptionAsync([FromRoute] Guid optionId)
        {
            var command = new DeletePollOptionCommand { PollOptionId = optionId };

            var deletedCount = await _mediator.Send(command);

            return Ok(new DefaultResponse($"Deleted: {deletedCount}"));
        }
    }
}