using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Models.SecuritySettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Service;

public class TokenService : IAuthService
{
    private readonly JwtAppSettings _jwtAppSettings;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public TokenService(
        IOptions<JwtAppSettings> jwtAppSettings,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _jwtAppSettings = jwtAppSettings.Value ?? throw new ArgumentNullException(nameof(jwtAppSettings));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task<JwtToken> Login(Login login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email);
        if (user == null)
        {
            throw new ArgumentException("Login inválido.");
        }

        if (!await _userManager.CheckPasswordAsync(user, login.Password))
        {                
            throw new ArgumentException("Login inválido.");
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = GetToken(authClaims);
        return new JwtToken(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
    }


    public async Task<bool> Register(CreateUser createUser)
    {
        if (createUser.Password != createUser.PasswordConfirm)
        {
            return false;
        }

        var registeredUser = await _userManager.FindByEmailAsync(createUser.Email);
        if (registeredUser != null)
        {
            throw new ArgumentException("Email já cadastrado.");
        }

        var user = new IdentityUser
        {
            UserName = createUser.Nome,
            Email = createUser.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, createUser.Password);
        if (result.Succeeded)
        {
            return true;
        }

        throw new Exception("Não foi possível criar o usuário.");
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAppSettings.Secret));

        var token = new JwtSecurityToken(
                expires: DateTime.Now.AddSeconds(600),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}