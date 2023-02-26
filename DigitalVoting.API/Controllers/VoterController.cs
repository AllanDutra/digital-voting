using DigitalVoting.Application.Commands.SignIn;
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

            return Ok(new DefaultResponse("Successfully registered Voter!"));
        }

        /// <summary>
        /// Generates a new access token to voter
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(SignInResponse), 200)]
        [ProducesResponseType(typeof(DefaultResponse), 400)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInCommand command)
        {
            string token = await _mediator.Send(command);

            return Ok(new SignInResponse(token));
        }
    }
}