namespace FitLife.Frontend.Models;

public class TrainingClass
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string DifficultyLevel { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int AvailableSpots { get; set; }
    public Guid TrainerId { get; set; }
    public List<ClassBooking> Bookings { get; set; } = [];
}
