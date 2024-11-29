using TaskFlow.Middlewares;
using Serilog;
using TaskFlow.Services;
using TaskFlow.Repositories;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Utilities;

var builder = WebApplication.CreateBuilder(args);

var logFilePath = $"logs/{DateTime.Now:yyyy-MM-dd}-myapp-development.log";
var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss}-[{Level}]-{Message:lj}{NewLine}{Exception}{NewLine}---------------------{NewLine}";

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File(
		logFilePath,
		outputTemplate: outputTemplate, //output format of each log
		rollingInterval: RollingInterval.Day,
		restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
	)
	.MinimumLevel.Debug()
	.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning) // Override logging for specific namespaces
	.Enrich.WithProperty("Application", "TaskFlow API") // Add custom property
	.Enrich.FromLogContext() // Include contextual data
	.CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllers(options =>
{
	options.Filters.Add<ValidationFilter>();
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
	options.SuppressModelStateInvalidFilter = true;
});


// Add Database Context
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
	options.UseSqlServer("Server=localhost,4001;Database=TaskFlow;User ID=sa;Password=@M1janinaok;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;"));

// Register Services and Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add Authentication and Authorization
builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
	opt.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = "http://localhost:5109",
		ValidAudience = "http://localhost:5000",
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});

builder.Services.AddAuthorization();

// Add Controllers and Swagger
// builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Exception Handling and HTTPS Redirection
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
}

// Use Authentication and Authorization
app.UseMiddleware<ApiResponseMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LoggingMiddleware>();

// Map Controllers
app.MapControllers();

app.Run();
