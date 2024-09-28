namespace TaskManagement.Data;

public class Comment
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public int TaskId { get; set; }
    public Task Task { get; set; }
}
