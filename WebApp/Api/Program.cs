using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api.Infrastructure.Data;
using Api.Infrastructure.Repositories;
using Api.Infrastructure.Repositories.Interfaces;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiContext") ?? throw new InvalidOperationException("Connection string 'ApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<ApiContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("ApiContext"))
    );

    builder.Services.AddScoped<ICityRepository, CityRepository>();

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
