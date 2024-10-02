using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagement.Data;

namespace TaskManagement.Services;

public sealed class ProjectsManagementService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ProjectsManagementService> _logger;

    public ProjectsManagementService(AppDbContext dbContext, ILogger<ProjectsManagementService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> CreateProjectAsync(string name, string description, DateTimeOffset timestamp)
    {
        try
        {
            Project project = new Project
            {
                Name = name,
                Description = description,
                Timestamp = timestamp
            };

            await _dbContext.Project.AddAsync(project).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error while creating project! See inner exception...");
            return false;
        }
    }

    public async Task<bool> UpdateProjectAsync(int id, string name, string description)
    {
        try
        {
            Project project = await _dbContext.Project.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
            if (project is null)
            {
                _logger.LogWarning("Project was not updated due to invalid id!");
                return false;
            }

            project.Update(name, description);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error while updating project! See inner exception...");
            return false;
        }
    }

    public async System.Threading.Tasks.Task DeleteProjectAsync(int id)
    {
        Project projects = await _dbContext.Project.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
        if (projects is null)
        {
            _logger.LogWarning("Project was not deleted because the provided id was invalid!");
            return;
        }

        _dbContext.Project.Remove(projects);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<ProjectInfo> GetProjectInfoAsync(int id)
    {
        Project project = await _dbContext.Project.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
        if (project is null)
        {
            return default;
        }

        List<string> tasks = project.Tasks.Select(x => x.Title).ToList();

        return new ProjectInfo(project.Name, project.Description, project.Timestamp, tasks);
    }
}

public class ProjectInfo
{
    public ProjectInfo(string name, string description, DateTimeOffset creationDate, List<string> tasks)
    {
        Name = name;
        Description = description;
        CreationDate = creationDate;
        Tasks = tasks;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTimeOffset CreationDate { get; set; }

    public List<string> Tasks { get; set; }
}
