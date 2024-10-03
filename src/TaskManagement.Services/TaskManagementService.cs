using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagement.Data;

namespace TaskManagement.Services;

public sealed class TaskManagementService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<TaskManagementService> _logger;

    public TaskManagementService(AppDbContext dbContext, ILogger<TaskManagementService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> UpdateTaskAsync(int taskId, string title, string description, string status, DateTimeOffset? dueDate)
    {
        try
        {
            Data.Task task = await _dbContext.Task.Where(x => x.Id == taskId).FirstOrDefaultAsync().ConfigureAwait(false); ;
            if (task is null)
            {
                _logger.LogWarning("Task was not updated due to invalid id!");
                return false;
            }

            if (Enum.TryParse(status, out Status parsedStatus) is false)
            {
                _logger.LogWarning("Invalid Status!");
                return false;
            }

            task.Update(title, description, parsedStatus, dueDate);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error while creating task! See inner exception...");
            return false;
        }
    }

    public async Task<bool> CreateTaskAsync(int ownerId, int projectId, string title, string description, string status, DateTimeOffset? dueDate, DateTimeOffset timestamp)
    {
        try
        {
            if (Enum.TryParse(status, out Status parsedStatus) is false)
            {
                _logger.LogWarning("Invalid Status!");
                return false;
            }

            Data.Task task = new Data.Task
            {
                Title = title,
                Description = description,
                Status = parsedStatus,
                DueDate = dueDate,
                Timestamp = timestamp,
                UserId = ownerId,
                ProjectId = projectId,
            };

            await _dbContext.Task.AddAsync(task).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error while creating task! See inner exception...");
            return false;
        }
    }

    public async Task<TaskInfo> GetTaskInfoAsync(int id)
    {
        Data.Task task = await _dbContext.Task.Where(x => x.Id == id).Include(x => x.User).FirstOrDefaultAsync().ConfigureAwait(false) ;
        if (task is null)
        {
            return default;
        }

        return new TaskInfo(task.Title, task.Description, task.Status, task.DueDate, task.Timestamp, task.User.Email);
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
    {
        Data.Task task = await _dbContext.Task.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
        if (task is null)
        {
            _logger.LogWarning("Task was not deleted because the provided id was invalid!");
            return;
        }

        _dbContext.Task.Remove(task);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}

public class TaskInfo
{
    public TaskInfo(string title, string description, Status status, DateTimeOffset? dueDate, DateTimeOffset timestamp, string user)
    {
        Title = title;
        Description = description;
        Status = status;
        DueDate = dueDate;
        Timestamp = timestamp;
        User = user;
    }

    public string Title { get; set; }

    public string Description { get; set; }

    public Status Status { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string User { get; set; }
}

