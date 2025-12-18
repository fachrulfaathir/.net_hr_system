using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Data;
using NetCoreApi.DTOs.Auth;
using NetCoreApi.Entities;
using NetCoreApi.Services;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IAuthService _authService;

    public AuthController(AppDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    //[HttpPost("register")]
    //public IActionResult Register(RegisterRequest request)
    //{
    //    if (_context.Users.Any(u => u.Email == request.Email))
    //        return BadRequest("Email already exists");

    //    var user = new User
    //    {
    //        Email = request.Email,
    //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
    //        Role = request.Role
    //    };

    //    _context.Users.Add(user);
    //    _context.SaveChanges();

    //    return Ok("Register success");
    //}

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        if(_context.Users.Any(u => u.Email == request.Email))
        {
            return BadRequest("Email Already Exist");
        }

        var user = new User
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("Register Success");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var token = _authService.Login(request.Email, request.Password);
        return Ok(new { token });
    }

}
