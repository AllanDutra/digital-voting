using Microsoft.EntityFrameworkCore;
using DigitalVoting.Core.Entities;

namespace DigitalVoting.Infrastructure.Configurations
{
    public class PollsConfigurations : IEntityTypeConfiguration<Poll>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Poll> entity)
        {
            entity.HasKey(e => e.Id).HasName("Poll_pkey");

            entity.ToTable("Poll");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnType("character varying");
        }
    }
}