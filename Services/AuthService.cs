using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WasteBinsAPI.Data.Repository;
using WasteBinsAPI.Models;
using WasteBinsAPI.ViewModel;

namespace WasteBinsAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IConfiguration configuration
        )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<string?> Authenticate(UserLoginViewModel user)
        {
            var isUserValid = await ValidateUserCredentialsAsync(user.Username, user.Password);
            if (!isUserValid)
            {
                return null;
            }

            var userModel = await _userRepository.GetUserByUsernameAsync(user.Username);
            return GenerateJwtToken(userModel!);
        }

        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return false;

            return _passwordHasher.VerifyPassword(password, user.Password);
        }

        private string? GenerateJwtToken(UserModel user)
        {
            byte[] secret = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"] ?? string.Empty);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            if (user.Role == null) return null;

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}