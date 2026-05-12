namespace FitLife.Frontend.Models;

public class Member
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PrimaryCenter { get; set; } = "Vesterbro";
    public string MembershipStatus { get; set; } = "Active";
    public DateTime StartDate { get; set; }
    public DateTime? CancellationDate { get; set; }
    public UserPreference? UserPreference { get; set; }
}

public class UserPreference
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public List<string> FitnessGoals { get; set; } = [];
    public List<string> TrainingInterests { get; set; } = [];
    public string MembershipType { get; set; } = "Premium";
    public bool ClassReminders { get; set; }
    public bool LivestreamReminders { get; set; }
    public bool BookingUpdates { get; set; }
    public bool CommunityActivity { get; set; }
    public bool MembershipAndPaymentMessages { get; set; }
}

public class UpdateMemberRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PrimaryCenter { get; set; } = "Vesterbro";
}

public class UpdateUserPreferenceRequest
{
    public List<string> FitnessGoals { get; set; } = [];
    public List<string> TrainingInterests { get; set; } = [];
    public string MembershipType { get; set; } = "Premium";
    public bool ClassReminders { get; set; }
    public bool LivestreamReminders { get; set; }
    public bool BookingUpdates { get; set; }
    public bool CommunityActivity { get; set; }
    public bool MembershipAndPaymentMessages { get; set; }
}