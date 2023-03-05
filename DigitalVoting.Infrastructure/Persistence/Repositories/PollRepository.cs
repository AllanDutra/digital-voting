using Microsoft.EntityFrameworkCore;
using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Interfaces.Repositories;
using Dapper;

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

        public async Task<int> GetNumberOfOptionsByVotingOptionId(Guid votingOptionId)
        {
            string query = "SELECT COUNT(VO.\"Id\") FROM \"VotingOption\" VO"
                         + " WHERE VO.\"Poll_Id\" ="
                         + " (SELECT SUBVO.\"Poll_Id\" FROM \"VotingOption\" SUBVO WHERE SUBVO.\"Id\" = @votingOptionId)";

            int numberOfOptions = await _dbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>(query, new { votingOptionId });

            return numberOfOptions;
        }

        public async Task<int> DeleteAsync(Guid pollId)
        {
            FormattableString sql = $"DELETE FROM \"Poll\" WHERE \"Id\" = {pollId}";

            int deletedCount = await _dbContext.Database.ExecuteSqlInterpolatedAsync(sql);

            return deletedCount;
        }

        public async Task<bool> CheckIfPollExists(Guid pollId)
        {
            Poll poll = await _dbContext.Polls.FirstOrDefaultAsync(p => p.Id == pollId);

            return poll != null;
        }

        public async Task<int> GetAmountOfVotesAsync(Guid pollId)
        {
            string query = "SELECT \"AmountOfVotes\" FROM \"Poll\" WHERE \"Id\" = @pollId";

            int amountOfVotes = await _dbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>(query, new { pollId });

            return amountOfVotes;
        }

        public async Task UpdateAmountOfVotesAsync(Guid pollId, int newAmountOfVotes)
        {
            FormattableString sql = $"UPDATE \"Poll\" SET \"AmountOfVotes\" = {newAmountOfVotes} WHERE \"Id\" = {pollId}";

            await _dbContext.Database.ExecuteSqlInterpolatedAsync(sql);
        }

        public async Task<bool> CheckIfUserAlreadyVotedInPollAsync(string voterUsername, Guid pollId)
        {
            string query = "SELECT COUNT(VVO.\"Id\") AS \"AlreadyVoted\" FROM \"Voter_VotingOption\" VVO WHERE VVO.\"Voter_Username\" = @voterUsername and VVO.\"Poll_Id\" = @pollId";

            int alreadyVoted = await _dbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<int>(query, new { voterUsername, pollId });

            return alreadyVoted == 1;
        }
    }
}