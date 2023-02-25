using DigitalVoting.Application.Commands.SignUp;
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

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommand command)
        {
            await _mediator.Send(command);

            return Ok(new { message = "Successfully registered Voter!" });
        }
    }
}