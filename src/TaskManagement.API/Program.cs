using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Services;
using Swashbuckle.AspNetCore;

namespace TaskManagement.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>(
          options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.RegisterTaskManagementServices();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1");
                c.RoutePrefix = string.Empty;
            });
        }


        app.Run();
    }
}
