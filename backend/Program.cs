using FluentValidation.AspNetCore;
using FluentValidation;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;
using JwtBackend.Validators;
using JwtBackend.Extensions;
using JwtBackend.Mapping;
using backend.DTO;
using backend.Validators;
using backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
});

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200");
    });
});

builder.Services.AddControllers();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();



builder.Services.AddApplicationServices();

builder.Services.AddValidators();

builder.Services.AddAutoMapper(typeof(UserProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
