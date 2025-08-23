using TaskManagement.Api.Middleware;
using TaskManagement.Application.BusinessLogic;
using TaskManagement.Application.Services;
using TaskManagement.Application.Repositories;
using TaskManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<ITodoTaskRepository, InMemoryTodoTaskRepository>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.AddScoped<TodoTaskBusinessLogic>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
