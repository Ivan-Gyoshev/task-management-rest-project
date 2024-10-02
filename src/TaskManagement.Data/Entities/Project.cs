namespace TaskManagement.Data;

public class Project
{
    public Project()
    {
        Tasks = new List<Task>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public ICollection<Task> Tasks { get; set; }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
