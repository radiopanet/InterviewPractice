using Microsoft.EntityFrameworkCore;
using InterviewPractice.Domain.Entities;

namespace InterviewPractice.Infrastructure.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {  }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<PracticeSession> PracticeSessions => Set<PracticeSession>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<QuestionType> QuestionTypes => Set<QuestionType>();
    public DbSet<SessionAnswer> SessionAnswers => Set<SessionAnswer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Map to existing Supabase tables (exact names)
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<PracticeSession>().ToTable("practice_sessions");
        modelBuilder.Entity<Profile>().ToTable("profiles");
        modelBuilder.Entity<Question>().ToTable("questions");
        modelBuilder.Entity<QuestionType>().ToTable("question_types");
        modelBuilder.Entity<SessionAnswer>().ToTable("session_answers");

        //JSONB columns
        modelBuilder.Entity<Question>()
            .Property(q => q.Options)
            .HasColumnType("jsonb");
            
        modelBuilder.Entity<Question>()
            .Property(q => q.CorrectAnswer)
            .HasColumnType("jsonb");

        modelBuilder.Entity<PracticeSession>()
            .Property(s => s.Filters)
            .HasColumnType("jsonb");

        modelBuilder.Entity<SessionAnswer>()
            .Property(a => a.UserAnswer)
            .HasColumnType("jsonb");   

    }
}