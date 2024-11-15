using TaskFlow.Middlewares;
using Serilog;
using Microsoft.Extensions.Logging;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Host.UseSerilog();

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

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.MapGet("/test", () =>
{
    return "Hello from /test! Checking hot reload, now its from windwos";
});


app.MapControllers();
app.UseHttpsRedirection();

app.Run();
