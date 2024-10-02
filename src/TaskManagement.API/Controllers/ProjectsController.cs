using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManagement.API.Helpers;
using TaskManagement.Services;

namespace TaskManagement.API.Controllers;

[Route("projects")]
public class ProjectsController : ApiControllerBase
{
    private readonly ProjectsManagementService _projectsService;

    public ProjectsController(ProjectsManagementService projectsService)
    {
        _projectsService = projectsService;
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        bool result = await _projectsService.CreateProjectAsync(request.Name, request.Description, DateTimeOffset.UtcNow);
        if (result is false)
            return StatusCode(500);

        return Created();
    }

    [HttpPut, Route("update")]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request)
    {
        bool result = await _projectsService.UpdateProjectAsync(request.Id, request.Name, request.Description);

        if (result is false)
            return NotFound("The project is not found!");

        return Ok();
    }

    [HttpGet, Route("info")]
    public async Task<IActionResult> GetProjectInfo([FromQuery, Required] int id)
    {
        var projectInfo = await _projectsService.GetProjectInfoAsync(id);

        if (projectInfo is null)
            return NotFound();

        return Ok(new ProjectInfoResponse(projectInfo.Name, projectInfo.Description, projectInfo.CreationDate, projectInfo.Tasks));
    }

    [HttpDelete, Route("delete")]
    public async Task<IActionResult> DeleteProject([FromQuery, Required] int id)
    {
        await _projectsService.DeleteProjectAsync(id);

        return NoContent();
    }
}

public class CreateProjectRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }
}

public class UpdateProjectRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }
}


public class ProjectInfoResponse
{
    public ProjectInfoResponse(string title, string description, DateTimeOffset timestamp, List<string> tasks)
    {
        Title = title;
        Description = description;
        CreationDate = timestamp;
        Tasks = tasks;
    }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTimeOffset CreationDate { get; set; }

    public List<string> Tasks { get; set; }
}