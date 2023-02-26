using MediatR;

namespace DigitalVoting.Application.Commands.DeletePollOption
{
    public class DeletePollOptionCommand : IRequest<int>
    {
        public Guid PollOptionId { get; set; }
    }
}