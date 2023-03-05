using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces.Repositories;

namespace DigitalVoting.Infrastructure.Persistence.Repositories
{
    public class VoterVotingOptionRepository : IVoterVotingOptionRepository
    {
        private readonly DigitalVotingDbContext _dbContext;
        public VoterVotingOptionRepository(DigitalVotingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(VoterVotingOption voterVotingOption)
        {
            await _dbContext.AddAsync(voterVotingOption);
        }
    }
}