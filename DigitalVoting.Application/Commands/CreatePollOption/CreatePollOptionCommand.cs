using MediatR;

namespace DigitalVoting.Application.Commands.CreatePollOption
{
    public class CreatePollOptionCommand : IRequest<Guid>
    {
        public Guid PollId { get; set; }
        
        public string Description { get; set; }
    }
}