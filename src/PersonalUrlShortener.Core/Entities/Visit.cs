namespace PersonalUrlShortener.Core.Entities;

public class Visit
{
    public long Id { get; set; }
    public long LinkId { get; set; }
    public DateTime Date { get; set; }
    public string IpAddress { get; set; } = default!;
    public string? Referrer { get; set; }
    public string? UserAgent { get; set; }
        
    public virtual Link Link { get; set; } = default!;
}