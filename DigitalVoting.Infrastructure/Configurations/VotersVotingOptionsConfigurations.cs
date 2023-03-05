using DigitalVoting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalVoting.Infrastructure.Configurations
{
    public class VotersVotingOptionsConfigurations : IEntityTypeConfiguration<VoterVotingOption>
    {
        public void Configure(EntityTypeBuilder<VoterVotingOption> entity)
        {
            entity.HasKey(e => e.Id).HasName("Voter_VotingOption_pkey");

            entity.ToTable("Voter_VotingOption");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PollId).HasColumnName("Poll_Id");
            entity.Property(e => e.VoterUsername)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("Voter_Username");
            entity.Property(e => e.VotingOptionId).HasColumnName("VotingOption_Id");

            entity.HasOne(d => d.VoterUsernameNavigation).WithMany(p => p.VoterVotingOptions)
                .HasForeignKey(d => d.VoterUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Voter_VotingOption_Voter_Username_fkey");

            entity.HasOne(d => d.VotingOption).WithMany(p => p.VoterVotingOptions)
                .HasForeignKey(d => d.VotingOptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Voter_VotingOption_VotingOption_Id_fkey");
        }
    }
}