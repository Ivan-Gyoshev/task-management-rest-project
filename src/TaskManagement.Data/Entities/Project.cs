namespace TaskManagement.Data;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public ICollection<Task> Tasks { get; set; }
}
