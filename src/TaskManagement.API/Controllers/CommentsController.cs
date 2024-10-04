using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManagement.API.Helpers;
using TaskManagement.Services;

namespace TaskManagement.API.Controllers;

[Route("comments")]
public class CommentsController : ApiControllerBase
{
    private readonly CommentsManagementService _service;

    public CommentsController(CommentsManagementService service)
    {
        _service = service;
    }

    [HttpGet, Route("for/task")]
    public async Task<IActionResult> GetTaskComments([FromQuery, Required] int taskId)
    {
        var taskComments = await _service.GetTaskCommentsAsync(taskId);

        return Ok(taskComments);
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
    {
        bool result = await _service.CreateCommentAsync(request.TaskId, request.Content, DateTimeOffset.UtcNow);
        if (result is false)
            return StatusCode(500);

        return Created();
    }

    [HttpPut, Route("update")]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentRequest request)
    {
        bool result = await _service.UpdateCommentAsync(request.Id, request.Content);

        if (result is false)
            return BadRequest("The user is invalid or the new email address is the same as the old one!");

        return Ok();
    }

    [HttpDelete, Route("delete")]
    public async Task<IActionResult> DeleteComment([FromQuery, Required] int id)
    {
        await _service.DeleteCommentAsync(id);

        return NoContent();
    }
}

public class CreateCommentRequest
{
    [Required]
    public int TaskId { get; set; }

    [Required]
    public string Content { get; set; }
}

public class UpdateCommentRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Content { get; set; }
}
