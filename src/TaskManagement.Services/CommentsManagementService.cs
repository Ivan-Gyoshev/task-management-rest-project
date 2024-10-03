using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagement.Data;

namespace TaskManagement.Services;

public sealed class CommentsManagementService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<CommentsManagementService> _logger;

    public CommentsManagementService(AppDbContext dbContext, ILogger<CommentsManagementService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> CreateCommentAsync(int taskId, string content, DateTimeOffset timestamp)
    {
        try
        {
            Comment comment = new Comment()
            {
                TaskId = taskId,
                Content = content,
                Timestamp = timestamp
            };

            await _dbContext.Comment.AddAsync(comment).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error while creating comment! See inner exception...");
            return false;
        }
    }

    public async Task<bool> UpdateCommentAsync(int id, string content)
    {
        try
        {
            Comment comment = await _dbContext.Comment.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
            if (comment is null)
            {
                _logger.LogWarning("Comment was not updated due to invalid id!");
                return false;
            }

            if (comment.Content.Equals(content))
                return false;

            comment.Content = content;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error while updating Comment! See inner exception...");
            return false;
        }
    }

    public async Task<List<CommentInfo>> GetTaskCommentsAsync(int taskId)
    {
        var comments = await _dbContext.Comment.Where(x => x.TaskId == taskId)
            .Select(x => new CommentInfo(x.Content, x.Timestamp))
            .ToListAsync()
            .ConfigureAwait(false);

        if (comments is null)
        {
            return default;
        }

        return comments;
    }

    public async System.Threading.Tasks.Task DeleteCommentAsync(int id)
    {
        Comment comment = await _dbContext.Comment.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
        if (comment is null)
        {
            _logger.LogWarning("Comment was not deleted because the provided id was invalid!");
            return;
        }

        _dbContext.Comment.Remove(comment);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}

public class CommentInfo
{
    public CommentInfo(string content, DateTimeOffset timestamp)
    {
        Content = content;
        Timestamp = timestamp;
    }

    public string Content { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}