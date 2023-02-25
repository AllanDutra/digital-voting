using DigitalVoting.Application.Commands.SignUp;
using DigitalVoting.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalVoting.API.Controllers
{
    [ApiController]
    [Route("api/voter")]
    public class VoterController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VoterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registers a new voter with his password and unique username
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("sign-up")]
        [ProducesResponseType(typeof(DefaultResponse), 200)]
        [ProducesResponseType(typeof(DefaultResponse), 400)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommand command)
        {
            await _mediator.Send(command);

            return Ok(new { message = "Successfully registered Voter!" });
        }
    }
}