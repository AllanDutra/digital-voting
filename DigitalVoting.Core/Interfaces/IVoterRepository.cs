using DigitalVoting.Core.Entities;

namespace DigitalVoting.Core.Interfaces
{
    public interface IVoterRepository
    {
        Task<bool> CheckIfUsernameAlreadyExistsAsync(string username);

        Task SignUpAsync(Voter voter);
    }
}