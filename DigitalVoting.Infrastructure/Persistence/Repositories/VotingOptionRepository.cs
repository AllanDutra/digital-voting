using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigitalVoting.Infrastructure.Persistence.Repositories
{
    public class VotingOptionRepository : IVotingOptionRepository
    {
        private readonly DigitalVotingDbContext _dbContext;
        public VotingOptionRepository(DigitalVotingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(VotingOption votingOption)
        {
            await _dbContext.VotingOptions.AddAsync(votingOption);
        }

        public async Task<int> DeleteAsync(Guid votingOptionId)
        {
            FormattableString sql = $"DELETE FROM \"VotingOption\" WHERE \"Id\" = {votingOptionId}";

            int deletedCount = await _dbContext.Database.ExecuteSqlInterpolatedAsync(sql);

            return deletedCount;
        }

        public async Task<int> DeleteByPollAsync(Guid pollId)
        {
            FormattableString sql = $"DELETE FROM \"VotingOption\" WHERE \"Poll_Id\" = {pollId}";

            int deletedCount = await _dbContext.Database.ExecuteSqlInterpolatedAsync(sql);

            return deletedCount;
        }
    }
}