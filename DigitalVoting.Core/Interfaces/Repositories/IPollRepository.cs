using DigitalVoting.Core.Entities;

namespace DigitalVoting.Core.Interfaces.Repositories
{
    public interface IPollRepository
    {
        Task AddAsync(Poll poll);

        Task<int> GetNumberOfOptionsByVotingOptionId(Guid votingOptionId);

        Task<int> DeleteAsync(Guid pollId);
    }
}