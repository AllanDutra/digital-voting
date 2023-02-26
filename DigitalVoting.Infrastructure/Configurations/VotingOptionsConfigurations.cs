using DigitalVoting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalVoting.Infrastructure.Configurations
{
    public class VotingOptionsConfigurations : IEntityTypeConfiguration<VotingOption>
    {
        public void Configure(EntityTypeBuilder<VotingOption> entity)
        {
            entity.HasKey(e => e.Id).HasName("VotingOption_pkey");

            entity.ToTable("VotingOption");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnType("character varying");
            entity.Property(e => e.PollId).HasColumnName("Poll_Id");

            entity.HasOne(d => d.Poll).WithMany(p => p.VotingOptions)
                .HasForeignKey(d => d.PollId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("VotingOption_Poll_Id_fkey");
        }
    }
}