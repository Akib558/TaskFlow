
using TaskFlow.Middlewares;
using Serilog;
using TaskFlow.Services;
using TaskFlow.Repositories;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Host.UseSerilog();

// builder.Services.AddSingleton<JwtHelper>(options => new JwtHelper(builder.Configuration));


builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlServer("Server=localhost, 4001;Database=TaskFlow;User ID=sa;Password=@M1janinaok;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;"));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

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

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
