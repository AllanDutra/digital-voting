using Microsoft.EntityFrameworkCore;
using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces.Repositories;

namespace DigitalVoting.Infrastructure.Persistence.Repositories
{
    public class PollRepository : IPollRepository
    {
        private readonly DigitalVotingDbContext _dbContext;
        public PollRepository(DigitalVotingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Poll poll)
        {
            await _dbContext.Polls.AddAsync(poll);
        }

        public async Task<int> DeleteAsync(Guid pollId)
        {
            FormattableString sql = $"DELETE FROM \"Poll\" WHERE \"Id\" = {pollId}";

            int deletedCount = await _dbContext.Database.ExecuteSqlInterpolatedAsync(sql);

            return deletedCount;
        }
    }
}