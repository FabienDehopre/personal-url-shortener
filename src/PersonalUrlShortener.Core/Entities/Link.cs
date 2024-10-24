namespace PersonalUrlShortener.Core.Entities;

public class Link
{
    public long Id { get; set; }
    public string Url { get; set; } = default!;
    public string ShortUrl { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? ExpiresIn { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string UserId { get; set; } = default!;
    public virtual ICollection<Visit> Visits { get; set; } = default!;
}