using DigitalVoting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalVoting.Infrastructure.Configurations
{
    public class VotesConfigurations : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> entity)
        {
            entity.HasNoKey();

            entity.Property(e => e.VoterUsername)
                .HasColumnType("character varying")
                .HasColumnName("Voter_Username");
            entity.Property(e => e.VotingOptionId)
                .HasColumnType("character varying")
                .HasColumnName("VotingOption_Id");

            entity.HasOne(d => d.VoterUsernameNavigation).WithMany()
                .HasForeignKey(d => d.VoterUsername)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Votes_Voter_Username_fkey");

            entity.HasOne(d => d.VotingOption).WithMany()
                .HasForeignKey(d => d.VotingOptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Votes_VotingOption_Id_fkey");
        }
    }
}