using DigitalVoting.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DigitalVoting.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _dbTransaction;
        private readonly DigitalVotingDbContext _dbContext;
        public UnitOfWork(DigitalVotingDbContext dbContext, IPollRepository pollRepository, IVotingOptionRepository votingOptionRepository)
        {
            _dbContext = dbContext;
            Polls = pollRepository;
            VotingOptions = votingOptionRepository;
        }

        public IPollRepository Polls { get; }

        public IVotingOptionRepository VotingOptions { get; }

        public async Task BeginTransactionAsync()
        {
            _dbTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _dbTransaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}