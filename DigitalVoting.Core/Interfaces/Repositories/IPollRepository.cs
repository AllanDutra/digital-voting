using DigitalVoting.Core.Entities;
using DigitalVoting.Core.Models;

namespace DigitalVoting.Core.Interfaces.Repositories
{
    public interface IPollRepository
    {
        Task AddAsync(Poll poll);

        Task<int> GetNumberOfOptionsByVotingOptionId(Guid votingOptionId);

        Task<int> DeleteAsync(Guid pollId);

        Task<bool> CheckIfPollExists(Guid pollId);

        Task<int> GetAmountOfVotesAsync(Guid pollId);

        Task UpdateAmountOfVotesAsync(Guid pollId, int newAmountOfVotes);

        Task<bool> CheckIfUserAlreadyVotedInPollAsync(string voterUsername, Guid pollId);

        Task<List<PollModel>> GetAllAsync();
    }
}