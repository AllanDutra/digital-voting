using DigitalVoting.Core.Entities;

namespace DigitalVoting.Core.Interfaces.Repositories
{
    public interface IVoterVotingOptionRepository
    {
        Task AddAsync(VoterVotingOption voterVotingOption);        
    }
}