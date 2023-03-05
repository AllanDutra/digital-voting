using DigitalVoting.Core.Interfaces.Repositories;

namespace DigitalVoting.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IPollRepository Polls { get; }

        IVotingOptionRepository VotingOptions { get; }

        IVoterVotingOptionRepository VotersVotingOptions { get; }

        Task CompleteAsync();

        Task BeginTransactionAsync();

        Task CommitAsync();
    }
}