using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api.Infrastructure.Data;
using Api.Infrastructure.Repositories;
using Api.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiContext") ?? throw new InvalidOperationException("Connection string 'ApiContext' not found."))
);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();
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

// Use CORS before other middleware
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
