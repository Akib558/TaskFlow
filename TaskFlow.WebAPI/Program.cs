using System.Diagnostics;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using TaskFlow.Core.Validators;
using TaskFlow.Data;
using TaskFlow.Middlewares;
using TaskFlow.Repositories;
using TaskFlow.Repositories.Project;
using TaskFlow.Repositories.Roles;
using TaskFlow.Services;
using TaskFlow.Services.Project;
using TaskFlow.Services.Role;
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
        builder.Configuration["ConnectionStrings:DefaultConnection"]
    // "Server=localhost,4001;Database=TaskFlow;User ID=sa;Password=@M1janinaok;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;"
    )
);

// Register Services and Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

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
            ValidAudience = "http://localhost:5109",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "")
            ),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add security definition
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter 'Bearer' followed by your token in the text input below.\nExample: Bearer <your_token>",
        }
    );

    // Add security requirement
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                Array.Empty<string>()
            },
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlow API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

<<<<<<< HEAD
// app.UseMiddleware<ExceptionHandlingMiddleware>();
// app.UseMiddleware<LoggingMiddleware>();
app.MapGet("/test", () =>
{
    return "Hello from /test! Checking hot reload, n windwos";
});


app.MapControllers();
=======
>>>>>>> 89a60e932ff559a5dea87b57c4df2abd8095e2b3
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiResponseMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

// Map Controllers
app.MapControllers();

app.Run();
