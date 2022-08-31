using EFCore.TrackingVsNoTracking.Console.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TrackingVsNoTracking.Console.Mappings
{
    public class PlayerMapping : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Player")
                   .HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasColumnName("Name")
                   .HasColumnType("VARCHAR(80)");

            builder.HasOne(p => p.Team)
                    .WithMany(p => p.Players);

            builder.Navigation(p => p.Team).AutoInclude();

        }
    }
}
