using BCT.BusinessRule.LogicServices;
using BCT.CommonLib.Services;
using BCT.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<BookingSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingSystemConnectionString")));

// Dependency Injection for Logic Services and Repositories
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ResponseService>();

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
