using System.Net;
using DigitalVoting.Core.Interfaces.Repositories;
using DigitalVoting.Shared.Exceptions;
using MediatR;

namespace DigitalVoting.Application.Commands.DeletePollOption
{
    public class DeletePollOptionCommandHandler : IRequestHandler<DeletePollOptionCommand, int>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IVotingOptionRepository _votingOptionRepository;
        public DeletePollOptionCommandHandler(IPollRepository pollRepository, IVotingOptionRepository votingOptionRepository)
        {
            _pollRepository = pollRepository;
            _votingOptionRepository = votingOptionRepository;
        }

        public async Task<int> Handle(DeletePollOptionCommand request, CancellationToken cancellationToken)
        {
            int pollNumberOfOptions = await _pollRepository.GetNumberOfOptionsByVotingOptionId(request.PollOptionId);

            if (pollNumberOfOptions <= 2)
                throw new ResponseException("Delete Failed. A poll must have at least two options.", HttpStatusCode.BadRequest);

            int deletedCount = await _votingOptionRepository.DeleteAsync(request.PollOptionId);

            return deletedCount;
        }
    }
}