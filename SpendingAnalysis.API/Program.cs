using Microsoft.EntityFrameworkCore;
using SpendingAnalysis.Aplication.Services;
using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.DataAccess;
using SpendingAnalysis.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SpendinAnalysisDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(SpendinAnalysisDbContext))));

builder.Services.AddScoped<ISpendingRepository, SpendingRepository>();
builder.Services.AddScoped<ISpendingAnalysisService, SpendingAnalysisService>();

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
