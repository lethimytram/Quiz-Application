using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(); 
builder.Services.AddControllers();

builder.Services.AddDbContext<QuizDatabase>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();             
    app.UseSwaggerUI();          
}

app.UseAuthorization(); 
app.MapControllers(); 

app.Run();