using DigitalVoting.Core.Interfaces.Repositories;
using DigitalVoting.Core.Models;
using MediatR;

namespace DigitalVoting.Application.Queries.GetAllPolls
{
    public class GetAllPollsQueryHandler : IRequestHandler<GetAllPollsQuery, List<PollModel>>
    {
        private readonly IPollRepository _pollRepository;
        public GetAllPollsQueryHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<List<PollModel>> Handle(GetAllPollsQuery request, CancellationToken cancellationToken)
        {
            return await _pollRepository.GetAllAsync();
        }
    }
}