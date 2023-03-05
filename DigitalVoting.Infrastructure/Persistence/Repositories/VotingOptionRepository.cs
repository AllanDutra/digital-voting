using Dapper;
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

        public async Task UpdateAmountOfVotesAsync(Guid votingOptionId, int newAmountOfVotes)
        {
            string sql = $"UPDATE \"VotingOption\" SET \"AmountOfVotes\" = @newAmountOfVotes WHERE \"Id\" = @votingOptionId";

            await _dbContext.Database.GetDbConnection().ExecuteAsync(sql, new
            {
                newAmountOfVotes,
                votingOptionId
            });
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetAmountOfVotesAsync(Guid votingOptionId)
        {
            string sql = $"SELECT \"AmountOfVotes\" FROM \"VotingOption\" WHERE \"Id\" = @votingOptionId";

            int amountOfVotes = await _dbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>(sql, new { votingOptionId });

            return amountOfVotes;
        }

        public async Task<Guid> GetPollIdAsync(Guid votingOptionId)
        {
            string query = "SELECT VO.\"Poll_Id\" FROM \"VotingOption\" VO WHERE VO.\"Id\" = @votingOptionId";

            Guid pollId = await _dbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<Guid>(query, new { votingOptionId });

            return pollId;
        }
    }
}