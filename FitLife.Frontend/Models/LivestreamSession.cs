namespace FitLife.Frontend.Models;

public class LivestreamSession
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Instructor { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public string TeamsUrl { get; set; } = string.Empty;
    public bool IsLive { get; set; }
    public int Participants { get; set; }
}