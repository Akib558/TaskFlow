using System.Diagnostics;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TaskFlow.Core.Validators;
using TaskFlow.Data;
using TaskFlow.Middlewares;
using TaskFlow.Repositories;
using TaskFlow.Services;
using TaskFlow.Utilites;

var builder = WebApplication.CreateBuilder(args);

var logFilePath = $"logs/{DateTime.Now:yyyy-MM-dd}-myapp-development.log";
var outputTemplate =
    "{Timestamp:yyyy-MM-dd HH:mm:ss}-[{Level}]-{Message:lj}{NewLine}{Exception}{NewLine}---------------------{NewLine}";

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        logFilePath,
        outputTemplate: outputTemplate,
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    )
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.WithProperty("Application", "TaskFlow API")
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<UserLoginAuthValidator>();
ApiModelValidation.AddValidationForModel(builder.Services);

builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlServer(
        "Server=localhost,4001;Database=TaskFlow;User ID=sa;Password=@M1janinaok;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;"
    )
);

// Register Services and Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Add Authentication and Authorization
builder
    .Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5109",
            ValidAudience = "http://localhost:5000",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "")
            ),
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var swaggerUrl =
        $"http://localhost:{app.Urls.FirstOrDefault()?.Split(':').LastOrDefault()}/swagger";
    OpenBrowser(swaggerUrl);
}

// Use Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiResponseMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

// Map Controllers
app.MapControllers();

app.Run();

//--
static void OpenBrowser(string url)
{
    try
    {
        Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
    }
    catch (Exception ex)
    {
        Log.Error("Failed to open Swagger UI: {ErrorMessage}", ex.Message);
    }
}
