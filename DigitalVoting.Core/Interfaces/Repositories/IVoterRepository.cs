using DigitalVoting.Core.Entities;

namespace DigitalVoting.Core.Interfaces.Repositories
{
    public interface IVoterRepository
    {
        Task<bool> CheckIfUsernameAlreadyExistsAsync(string username);

        Task SignUpAsync(Voter voter);

        Task<Voter> CheckIfVoterExistsAsync(Voter searchedVoter);
    }
}