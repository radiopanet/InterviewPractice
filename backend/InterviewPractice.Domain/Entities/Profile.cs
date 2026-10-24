
namespace InterviewPractice.Domain.Entities;

public class Profile
{
    public Guid Id { get; set; }
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    //Navigation
    public ICollection<PracticeSession> PracticeSessions {get; set;} = new List<PracticeSessions>();
}