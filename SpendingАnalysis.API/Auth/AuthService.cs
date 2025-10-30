using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpendingAnalysis.DataAccess;
using SpendingAnalysis.DataAccess.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.Aplication.Auth;

namespace SpendingAnalysis.Aplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly SpendinAnalysisDbContext _context;
        private ICategoriesService _categoriesService;
        private readonly string _jwtKey;

        public AuthService(SpendinAnalysisDbContext context, IConfiguration config, ICategoriesService categoriesService)
        {
            _context = context;
            _jwtKey = config["Jwt:Key"] ?? throw new Exception("JWT Key not configured");
            _categoriesService = categoriesService;
        }

        public async Task<AuthResponse> RegisterAsync(string username, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
                throw new Exception("Username already exists");

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            //Добавить дефолтные категории для пользователя
            await _categoriesService.InsertDefaultCategories(user.Id);

            var token = GenerateJwtToken(user);
            return new AuthResponse { Token = token, Username = user.Username };
        }

        public async Task<AuthResponse> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new Exception("Invalid username or password");

            var token = GenerateJwtToken(user);
            return new AuthResponse { Token = token, Username = user.Username };
        }

        private string GenerateJwtToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // 🆔 ID пользователя
                new Claim(ClaimTypes.Name, user.Username)                 // 👤 Имя пользователя
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
