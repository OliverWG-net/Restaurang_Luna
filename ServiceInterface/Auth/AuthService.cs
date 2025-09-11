using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurang_luna.DTOs.Admin.Request;
using Restaurang_luna.DTOs.Admin.Response;
using Restaurang_luna.Data;
using Restaurang_luna.ServiceInterface.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Restaurang_luna.ServiceInterface.Auth
{
    public class AuthService : IAuthService
    {
        private readonly LunaDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _hasher;
        private readonly IHttpContextAccessor _httpContext;

        public AuthService(LunaDbContext dbContext, IConfiguration configuration, IPasswordHasher hasher, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _hasher = hasher;
            _httpContext = httpContext;
        }

        public async Task<LoginResponseDto> Authenticate(LoginRequestDto request, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Username of password is null");

            var AdminAccount = await _dbContext.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.UserName == request.UserName, ct);

            if (AdminAccount == null || !_hasher.Verify(request.Password, AdminAccount.PasswordHash))
                throw new AuthenticationException("Not account was found");

            //get config from appsettings
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimestamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            //creates token descriptor with claims and signing creds
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, request.UserName)
                }),
                Expires = tokenExpiryTimestamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature),
            };

            //create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDecriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            //config cookie options for storing
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            //append token to http cookie
            _httpContext.HttpContext!.Response.Cookies.Append("AuthToken", accessToken, cookieOptions);

            //return response with token
            return new LoginResponseDto
            {
                LoginSuccess = true,
                AccessToken = accessToken,
                UserName = request.UserName,
                ExpiresIn = (int)tokenExpiryTimestamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
        public async Task Logout(CancellationToken ct = default)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            };

            //added options for when adding path and domain
            _httpContext.HttpContext!.Response.Cookies.Delete("AuthToken", options);
            await Task.CompletedTask;
        }
    }
}
