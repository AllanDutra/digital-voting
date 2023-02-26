using MediatR;

namespace DigitalVoting.Application.Commands.DeletePoll
{
    public class DeletePollCommand : IRequest<int>
    {
        public Guid PollId { get; set; }
    }
}