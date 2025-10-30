using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpendingAnalysis.Aplication.Services;
using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.DataAccess;
using SpendingAnalysis.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
// ? ��������� ����������� ����� ����� �������� builder
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// ?? ����������� JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "super_secret_key_123456789";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://85.113.58.165:4200", 
            "http://localhost:4200", 
            "http://192.168.0.105:4200", 
            "https://85.113.58.165:4200", 
            "https://localhost:4200", 
            "https://192.168.0.105:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // ���� ����������� cookies ��� Authorization header
    });
});



builder.Services.AddDbContext<SpendinAnalysisDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(SpendinAnalysisDbContext))));

builder.Services.AddScoped<ISpendingRepository, SpendingRepository>();
builder.Services.AddScoped<ISpendingAnalysisService, SpendingAnalysisService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");


// ? ����������� ������ ���� �� UseAuthorization
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.MapMethods("{*path}", new[] { "OPTIONS" }, () => Results.Ok()).AllowAnonymous();

app.Run();
