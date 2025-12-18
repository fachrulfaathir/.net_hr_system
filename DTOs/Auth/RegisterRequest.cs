using NetCoreApi.Entities.Enums;

namespace NetCoreApi.DTOs.Auth
{
    public class RegisterRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Role Role { get; set; } = Role.Employee;
    }

}
