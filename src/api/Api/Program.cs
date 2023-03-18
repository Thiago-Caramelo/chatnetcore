using AutoMapper;
using Business.Interfaces;
using Business.Services;
using ChatNetCore.Data;
using ChatNetCore.Models;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IChatRepository, ChatRepository>();
builder.Services.AddTransient<IChatService, ChatService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var chatString = builder.Configuration.GetConnectionString("ChatConnection");
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer(chatString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    // base-address of your identityserver
    options.Authority = "https://localhost:7127";
    options.TokenValidationParameters.ValidateAudience = false;
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(ChatRepository));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allow_any",
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("allow_any");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
