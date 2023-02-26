using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces;
using MediatR;

namespace DigitalVoting.Application.Commands.CreateNewPoll
{
    public class CreateNewPollCommandHandler : IRequestHandler<CreateNewPollCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateNewPollCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateNewPollCommand request, CancellationToken cancellationToken)
        {
            Guid newPollId = Guid.NewGuid();

            Poll poll = new Poll
            {
                AmountOfVotes = 0,
                Description = request.PollDescription,
                Id = newPollId
            };

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.Polls.AddAsync(poll);

            await _unitOfWork.CompleteAsync();

            request.PollVotingOptions.ForEach(async vo =>
            {
                VotingOption votingOption = new VotingOption
                {
                    AmountOfVotes = 0,
                    Description = vo,
                    Id = Guid.NewGuid(),
                    PollId = newPollId
                };

                await _unitOfWork.VotingOptions.AddAsync(votingOption);
            });

            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return poll.Id;
        }
    }
}