using DigitalVoting.Core.Entities;

namespace DigitalVoting.Core.Interfaces.Repositories
{
    public interface IVotingOptionRepository
    {
        Task AddAsync(VotingOption votingOption);

        Task<int> DeleteAsync(Guid votingOptionId);

        Task<int> DeleteByPollAsync(Guid pollId);
    }
}