using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.ComponentModel.DataAnnotations;
using TaskManagement.API.Helpers;
using TaskManagement.Data;
using TaskManagement.Services;

namespace TaskManagement.API.Controllers;

[Route("users")]
public class UsersController : ApiControllerBase
{
    private readonly UsersManagementService _usersService;

    public UsersController(UsersManagementService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        bool result = await _usersService.CreateUserAsync(request.Email, request.Password);
        if (result is false)
            return StatusCode(500);

        return Created();
    }

    [HttpPut, Route("update/email")]
    public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailRequest request)
    {
        bool result = await _usersService.UpdateUserEmailAsync(request.Id, request.NewEmail);

        if (result is false)
            return BadRequest("The user is invalid or the new email address is the same as the old one!");

        return Ok();
    }

    [HttpPut, Route("update/password")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordRequest request)
    {
        bool result = await _usersService.UpdateUserPasswordAsync(request.Id, request.Password);

        if (result is false)
            return BadRequest("The user is invalid or the new password is the same as the old one!");

        return Ok();
    }

    [HttpGet, Route("info")]
    public async Task<IActionResult> GetUserInfo([FromQuery, Required] int id)
    {
        var userInfo = await _usersService.GetUserInfoAsync(id);

        if (userInfo is null)
            return NotFound();

        return Ok(new UserInfoResponse(userInfo.Email, userInfo.Tasks));
    }

    [HttpDelete, Route("delete")]
    public async Task<IActionResult> DeleteUser([FromQuery, Required] int id)
    {
        await _usersService.DeleteUserAsync(id);

        return NoContent();
    }
}

public class CreateUserRequest
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(6)]
    public string Password { get; set; }
}

public class UpdateUserEmailRequest
{
    [Required]
    public int Id { get; set; }

    [Required, EmailAddress]
    public string NewEmail { get; set; }
}

public class UpdateUserPasswordRequest
{
    [Required]
    public int Id { get; set; }

    [Required, MinLength(6)]
    public string Password { get; set; }
}

public class UserInfoResponse
{
    public UserInfoResponse(string email, List<string> tasks)
    {
        Email = email;
        Tasks = tasks;
    }

    public string Email { get; set; }

    public List<string> Tasks { get; set; }
}