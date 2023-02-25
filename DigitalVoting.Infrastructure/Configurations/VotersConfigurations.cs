using DigitalVoting.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalVoting.Infrastructure.Configurations
{
    public class VotersConfigurations : IEntityTypeConfiguration<Voter>
    {
        public void Configure(EntityTypeBuilder<Voter> entity)
        {
            entity.HasKey(e => e.Username).HasName("Voter_pkey");

            entity.ToTable("Voter");

            entity.Property(e => e.Username).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");
        }
    }
}