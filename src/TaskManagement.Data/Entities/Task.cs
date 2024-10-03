namespace TaskManagement.Data;

public class Task
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Status Status { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public ICollection<Comment> Comments { get; set; }

    public void Update(string title, string description, Status status, DateTimeOffset? dueDate)
    {
        Title = title;
        Description = description;
        Status = status;
        DueDate = dueDate;
    }
}

public enum Status
{
    Todo,
    InProgress,
    Completed
}