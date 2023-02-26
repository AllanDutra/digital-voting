using MediatR;

namespace DigitalVoting.Application.Commands.CreateNewPoll
{
    public class CreateNewPollCommand : IRequest<Guid>
    {
        public string PollDescription { get; set; }
        
        public List<string> PollVotingOptions { get; set; }
    }
}