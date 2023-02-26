using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigitalVoting.Infrastructure.Persistence.Repositories
{
    public class VoterRepository : IVoterRepository
    {
        private readonly DigitalVotingDbContext _dbContext;
        public VoterRepository(DigitalVotingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckIfUsernameAlreadyExistsAsync(string username)
        {
            var voter = await _dbContext.Voters.FirstOrDefaultAsync(v => v.Username.ToLower() == username.ToLower());

            return voter != null;
        }

        public async Task SignUpAsync(Voter voter)
        {
            await _dbContext.Voters.AddAsync(voter);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Voter> CheckIfVoterExistsAsync(Voter searchedVoter)
        {
            var voter = await _dbContext.Voters
                .Where(v => v.Username.ToLower() == searchedVoter.Username.ToLower() && v.Password == searchedVoter.Password)
                .SingleOrDefaultAsync();

            return voter;
        }
    }
}