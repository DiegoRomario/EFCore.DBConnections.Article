using EFCore.TrackingVsNoTracking.Console.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TrackingVsNoTracking.Console.Mappings
{
    public class TeamMapping : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Team")
                 .HasKey(m => m.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasColumnName("Name")
                   .HasColumnType("VARCHAR(50)");

            builder.HasMany(p => p.Players)
                   .WithOne(p => p.Team);

            builder.Navigation(p => p.Players).AutoInclude();

        }
    }
}
