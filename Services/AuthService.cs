using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetCoreApi.Data;
using NetCoreApi.Entities;
using NetCoreApi.Entities.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;




namespace NetCoreApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;
        public AuthService(AppDbContext context, IConfiguration config) {
            _appDbContext = context;
            _config = config;
        }

        public string Login(String email, String password)
        {
            var user = _appDbContext.Users.SingleOrDefault(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:key"]!)
                );
            var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public void Register(string email, string password, int role)
        //{
        //    if (_appDbContext.Users.Any(u => u.Email == email))
        //        throw new Exception("Email already exists");

        //    var user = new User
        //    {
        //        Email = email,
        //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
        //        Role = (Role)role
        //    };

        //    _appDbContext.Users.Add(user);
        //    _appDbContext.SaveChanges();
        //}

        public void Register(string email, string password, int role)
        {
            if(_appDbContext.Users.Any(u=> u.Email == email))
            {
                throw new Exception("Email Already Exist");
            }

            var user = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = (Role)role
            };

            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
        }

    }
}
