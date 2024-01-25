
using Microsoft.EntityFrameworkCore;
using USER_SERVICE.Modal;
using USER_SERVICE.Repositories;
using USER_SERVICE.Services;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5001); // Port HTTP
    options.ListenAnyIP(5002, listenOptions =>
    {
        listenOptions.UseHttps(); // Port HTTPS
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyAppDbContext>(connection => connection.UseSqlServer(builder.Configuration.GetConnectionString("apicon")));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
