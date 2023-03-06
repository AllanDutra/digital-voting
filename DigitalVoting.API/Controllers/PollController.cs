using DigitalVoting.Application.Commands.CreateNewPoll;
using DigitalVoting.Application.Commands.CreatePollOption;
using DigitalVoting.Application.Commands.DeletePoll;
using DigitalVoting.Application.Commands.DeletePollOption;
using DigitalVoting.Application.Queries.GetAllPolls;
using DigitalVoting.Core.Models;
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
        /// Creates a new poll option for an existing poll.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create-option")]
        [ProducesResponseType(typeof(CreateOptionResponse), 200)]
        [ProducesResponseType(typeof(DefaultResponse), 400)]
        [ProducesResponseType(typeof(void), 401)]
        public async Task<IActionResult> CreatePollOptionAsync([FromBody] CreatePollOptionCommand command)
        {
            var newVotingOptionId = await _mediator.Send(command);

            return Ok(new CreateOptionResponse("A new option has been created for informed voting!", newVotingOptionId));
        }

        /// <summary>
        /// Gets all polls with all their voting options
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(List<PollModel>), 200)]
        [ProducesResponseType(typeof(void), 401)]
        public async Task<IActionResult> GetAllPollsAsync()
        {
            GetAllPollsQuery query = new GetAllPollsQuery();

            var polls = await _mediator.Send(query);

            return Ok(polls);
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

        /// <summary>
        /// Deletes a poll and all of its options
        /// </summary>
        /// <param name="pollId">Id of the poll you want to delete</param>
        /// <returns></returns>
        [HttpDelete("delete/{pollId}")]
        [ProducesResponseType(typeof(DefaultResponse), 200)]
        [ProducesResponseType(typeof(DefaultResponse), 400)]
        [ProducesResponseType(typeof(void), 401)]
        public async Task<IActionResult> DeletePollAsync([FromRoute] Guid pollId)
        {
            var command = new DeletePollCommand { PollId = pollId };

            var numberOfDeletedOptions = await _mediator.Send(command);

            return Ok(new DefaultResponse($"Poll deleted with your {numberOfDeletedOptions} options."));
        }
    }
}