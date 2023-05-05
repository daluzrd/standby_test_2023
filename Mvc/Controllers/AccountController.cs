using Microsoft.AspNetCore.Mvc;
using Mvc.Controllers.Base;
using Mvc.DataService.Interface;
using Mvc.Models.Account;

namespace Mvc.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
        ViewBag.IsLogged = false;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        try
        {
            var jwtToken = await _accountService.Login(loginViewModel);
            SetToken(jwtToken);

            return Redirect("~/Home/Index");
        }
        catch (Exception e)
        {
            ViewData["Error"] = e.Message;

            return View();
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        try
        {
            await _accountService.Register(registerViewModel);

            return RedirectToAction("Login");
        }
        catch (Exception e)
        {
            ViewData["Error"] = e.Message;

            return View();
        }
    }

    [HttpGet]
    public IActionResult Logout()
    {
        ClearToken();
        return Redirect("/Account/Login");
    }
    

    private void SetToken(JwtToken jwtToken)
    {
        HttpContext.Session.SetString("JwtToken", jwtToken.AccessToken);
        HttpContext.Session.SetString("ExpiresIn", jwtToken.ExpiresIn.ToString());
    }
}