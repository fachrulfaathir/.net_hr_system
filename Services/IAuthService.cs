namespace NetCoreApi.Services
{
    public interface IAuthService
    {
        string Login(string email, string password);
        void Register(string email, string password, int role);
    }
}
