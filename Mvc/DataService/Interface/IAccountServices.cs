using Mvc.Models.Account;

namespace Mvc.DataService.Interface;

public interface IAccountService
{
    Task<JwtToken> Login(LoginViewModel loginViewModel);
    Task Register(RegisterViewModel registerViewModel);
}