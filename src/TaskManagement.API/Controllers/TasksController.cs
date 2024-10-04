using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManagement.API.Helpers;
using TaskManagement.Services;

namespace TaskManagement.API.Controllers;

[Route("tasks")]
public class TasksController : ApiControllerBase
{
    private readonly TaskManagementService _taskService;

    public TasksController(TaskManagementService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskRequest request)
    {
        bool result = await _taskService.CreateTaskAsync(request.OwnerId, request.ProjectId, request.Title, request.Description, request.Status, request.DueDate, DateTimeOffset.UtcNow);
        if (result is false)
            return StatusCode(500);

        return Created();
    }

    [HttpPut, Route("update")]
    public async Task<IActionResult> UpdateTaskAsync([FromBody] UpdateTaskRequest request)
    {
        bool result = await _taskService.UpdateTaskAsync(request.Id, request.Title, request.Description, request.Status, request.DueDate);

        if (result is false)
            return NotFound("The task is not found!");

        return Ok();
    }

    [HttpGet, Route("info")]
    public async Task<IActionResult> GetTaskInfo([FromQuery, Required] int id)
    {
        var taskInfo = await _taskService.GetTaskInfoAsync(id);

        if (taskInfo is null)
            return NotFound();

        return Ok(new TaskInfoResponse(taskInfo.Title, taskInfo.Description, taskInfo.Status.ToString(), taskInfo.DueDate, taskInfo.Timestamp, taskInfo.User));
    }

    [HttpDelete, Route("delete")]
    public async Task<IActionResult> DeleteProject([FromQuery, Required] int id)
    {
        await _taskService.DeleteTaskAsync(id);

        return NoContent();
    }
}

public class CreateTaskRequest
{
    [Required]
    public int OwnerId { get; set; }

    [Required]
    public int ProjectId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required, AllowedValues("Todo", "InProgress", "Completed")]
    public string Status { get; set; }

    public DateTimeOffset? DueDate { get; set; }
}

public class UpdateTaskRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required, AllowedValues("Todo", "InProgress", "Completed")]
    public string Status { get; set; }

    public DateTimeOffset? DueDate { get; set; }
}

public class TaskInfoResponse
{
    public TaskInfoResponse(string title, string description, string status, DateTimeOffset? dueDate, DateTimeOffset timestamp, string user)
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

    public string Status { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string User { get; set; }
}
