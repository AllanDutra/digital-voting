using DigitalVoting.Core.Interfaces.Repositories;

namespace DigitalVoting.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        IPollRepository Polls { get; }

        IVotingOptionRepository VotingOptions { get; } 

        Task CompleteAsync();

        Task BeginTransactionAsync();

        Task CommitAsync();
    }
}