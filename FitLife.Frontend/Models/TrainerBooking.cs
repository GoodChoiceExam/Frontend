namespace FitLife.Frontend.Models;

public class TrainerBooking
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid TrainerId { get; set; }
    public DateTime SessionTime { get; set; }
    public DateTime BookedAt { get; set; }
    public string Status { get; set; } = string.Empty;
}