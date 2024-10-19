using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalUrlShortener.Core.Entities;

namespace PersonalUrlShortener.Infrastructure.Data.Config;

public class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
        builder.ToTable("Visits");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Date)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.IpAddress)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(255);
        builder.Property(x => x.Referrer)
            .IsRequired(false)
            .IsUnicode()
            .HasMaxLength(2048);
        builder.Property(x => x.UserAgent)
            .IsRequired(false)
            .IsUnicode()
            .HasMaxLength(2048);
        builder.HasOne(x => x.Link)
            .WithMany(x => x.Visits)
            .HasForeignKey(x => x.LinkId);
    }
}