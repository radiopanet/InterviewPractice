
namespace InterviewPractice.Domain.Entities;

public class Category
{
    public int Id {get; set;}
    public string Name {get; set;} = null!;
    public string slug {get; set;} = null!; //matches db column "slug"
    public string Description {get; set;}
    public DateTime CreatedAt {get; set;}

    //Navigation
    public ICollection<Question> Questions = new List<Question>();
    
}