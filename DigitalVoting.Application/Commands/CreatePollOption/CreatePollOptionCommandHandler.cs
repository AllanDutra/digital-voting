using System.Net;
using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces.Repositories;
using DigitalVoting.Shared.Exceptions;
using MediatR;

namespace DigitalVoting.Application.Commands.CreatePollOption
{
    public class CreatePollOptionCommandHandler : IRequestHandler<CreatePollOptionCommand, Guid>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IVotingOptionRepository _votingOptionRepository;
        public CreatePollOptionCommandHandler(IPollRepository pollRepository, IVotingOptionRepository votingOptionRepository)
        {
            _pollRepository = pollRepository;
            _votingOptionRepository = votingOptionRepository;
        }

        public async Task<Guid> Handle(CreatePollOptionCommand request, CancellationToken cancellationToken)
        {
            bool pollExists = await _pollRepository.CheckIfPollExists(request.PollId);

            if (pollExists == false)
                throw new ResponseException("Please enter a valid poll id", HttpStatusCode.BadRequest);

            Guid votingOptionId = Guid.NewGuid();

            VotingOption votingOption = new VotingOption
            {
                AmountOfVotes = 0,
                Description = request.Description,
                Id = votingOptionId,
                PollId = request.PollId
            };

            await _votingOptionRepository.AddAsync(votingOption);

            await _votingOptionRepository.SaveChangesAsync();

            return votingOptionId;
        }
    }
}