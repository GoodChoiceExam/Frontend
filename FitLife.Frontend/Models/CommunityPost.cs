namespace FitLife.Frontend.Models;

public class CommunityPost
{
    public Guid Id { get; set; }
    public Guid CommunityId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}