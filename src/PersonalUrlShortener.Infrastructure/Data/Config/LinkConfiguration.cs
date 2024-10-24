using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalUrlShortener.Core.Entities;
using PersonalUrlShortener.Infrastructure.Data.Generators;

namespace PersonalUrlShortener.Infrastructure.Data.Config;

public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.ToTable("Links");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasValueGenerator<IdGenValueGenerator>()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Url)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(4096);
        builder.Property(x => x.ShortUrl)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(100);
        builder.Property(x => x.Description)
            .IsRequired(false)
            .IsUnicode()
            .HasMaxLength(4096);
        builder.Property(x => x.ExpiresIn)
            .IsRequired(false);
        builder.Property(x => x.PasswordHash)
            .IsRequired(false);
        builder.Property(x => x.PasswordSalt)
            .IsRequired(false);
        builder.Property(x => x.UserId)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(64);
        
        builder.HasIndex(x => x.ShortUrl).IsUnique();
    }
}