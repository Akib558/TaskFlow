
using TaskFlow.Middlewares;
using Serilog;
using TaskFlow.Services;
using TaskFlow.Repositories;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlServer("Server=localhost, 4001;Database=TaskFlow;User ID=sa;Password=@M1janinaok;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;"));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


var app = builder.Build();


Log.Information("Test log entry outside middleware");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseMiddleware<ExceptionHandlingMiddleware>();
// app.UseMiddleware<LoggingMiddleware>();
app.MapGet("/test", () =>
{
    return "Hello from /test! Checking hot reload, now its from windwos";
});


app.MapControllers();
app.UseHttpsRedirection();

app.Run();
