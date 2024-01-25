using Microsoft.EntityFrameworkCore;
using TASK_SERVICE.Entities;
using TASK_SERVICE.Repositories;
using TASK_SERVICE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5129); // Port HTTP
    options.ListenAnyIP(7201, listenOptions =>
    {
        listenOptions.UseHttps(); // Port HTTPS
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddDbContext<TaskDbContext>(connection => connection.UseSqlServer(builder.Configuration.GetConnectionString("apicon")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
