using System.Reflection;
using DigitalVoting.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalVoting.Infrastructure.Persistence
{
    public class DigitalVotingDbContext : DbContext
    {
        public DigitalVotingDbContext(DbContextOptions<DigitalVotingDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Poll> Polls { get; set; }

        public virtual DbSet<Vote> Votes { get; set; }

        public virtual DbSet<Voter> Voters { get; set; }

        public virtual DbSet<VotingOption> VotingOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}