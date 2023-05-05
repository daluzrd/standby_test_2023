using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Service
{
    public interface IAuthService
    {
        Task<JwtToken> Login(Login login);
        Task<bool> Register(CreateUser createUser);
        Task Logout();
    }
}