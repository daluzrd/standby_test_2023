using Infrastructure.Identity.Models;
using Infrastructure.Identity.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(
        IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(CreateUser createUser)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Cadastro inválido");
            }

            if (await _authService.Register(createUser))
            {
                return Ok();
            }

            return BadRequest("Cadastro inválido.");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(Login login)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Login inválido.");
            }

            return Ok(await _authService.Login(login));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return Ok();
    }
}