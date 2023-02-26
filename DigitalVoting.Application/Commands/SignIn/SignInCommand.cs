using MediatR;

namespace DigitalVoting.Application.Commands.SignIn
{
    public class SignInCommand : IRequest<string>
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}