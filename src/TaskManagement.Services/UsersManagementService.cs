using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagement.Data;

namespace TaskManagement.Services;

public sealed class UsersManagementService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<UsersManagementService> _logger;

    public UsersManagementService(AppDbContext appDbContext, ILogger<UsersManagementService> logger)
    {
        _dbContext = appDbContext;
        _logger = logger;
    }

    public async Task<bool> CreateUserAsync(string email, string password)
    {
        try
        {
            User user = new User()
            {
                Email = email,
                Password = password
            };

            await _dbContext.User.AddAsync(user).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error while creating user! See inner exception...");
            return false;
        }
    }

    public async Task<bool> UpdateUserEmailAsync(int id, string newEmail)
    {
        try
        {
            User user = await _dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
            if (user is null)
            {
                _logger.LogWarning("User email was not updated due to invalid id!");
                return false;
            }

            if (user.Email.Equals(newEmail))
                return false;

            user.Email = newEmail;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error while updating user email! See inner exception...");
            return false;
        }
    }

    public async Task<bool> UpdateUserPasswordAsync(int id, string newPassword)
    {
        try
        {
            User user = await _dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
            if (user is null)
            {
                _logger.LogWarning("User password was not updated due to invalid id!");
                return false;
            }

            if (user.Password.Equals(newPassword))
                return false;

            user.Password = newPassword;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error while updating user password! See inner exception...");
            return false;
        }
    }

    public async Task<UserInfo> GetUserInfoAsync(int id)
    {
        User user = await _dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
        if (user is null)
        {
            return default;
        }

        List<string> tasks = await _dbContext.Task.Where(t => t.UserId == user.Id).Select(x => x.Title).ToListAsync().ConfigureAwait(false);

        return new UserInfo(user.Email, tasks);
    }

    public async System.Threading.Tasks.Task DeleteUserAsync(int id)
    {
        User user = await _dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false); ;
        if (user is null)
        {
            _logger.LogWarning("User was not deleted because the provided id was invalid!");
            return;
        }

        _dbContext.User.Remove(user);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}

public class UserInfo
{
    public UserInfo(string email, List<string> tasks)
    {
        Email = email;
        Tasks = tasks;
    }

    public string Email { get; set; }

    public List<string> Tasks { get; set; }
}
