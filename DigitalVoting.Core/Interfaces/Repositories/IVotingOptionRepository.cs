using DigitalVoting.Core.Entities;

namespace DigitalVoting.Core.Interfaces.Repositories
{
    public interface IVotingOptionRepository
    {
        Task AddAsync(VotingOption votingOption);

        Task<int> DeleteAsync(Guid votingOptionId);

        Task<int> DeleteByPollAsync(Guid pollId);

        Task UpdateAmountOfVotesAsync(Guid votingOptionId, int newAmountOfVotes);

        Task<int> GetAmountOfVotesAsync(Guid votingOptionId);

        Task SaveChangesAsync();

        Task<Guid> GetPollIdAsync(Guid votingOptionId);
    }
}