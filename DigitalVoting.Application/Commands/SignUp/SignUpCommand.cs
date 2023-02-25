using MediatR;

namespace DigitalVoting.Application.Commands.SignUp
{
    public class SignUpCommand : IRequest<Unit>
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}