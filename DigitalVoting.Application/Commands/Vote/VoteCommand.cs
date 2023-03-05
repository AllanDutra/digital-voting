using MediatR;

namespace DigitalVoting.Application.Commands.VoteCommand
{
    public class VoteCommand : IRequest
    {
        /// <summary>
        /// Id of the option you want to vote for
        /// </summary>
        /// <value></value>
        public Guid VotingOption_Id { get; set; }
    }
}