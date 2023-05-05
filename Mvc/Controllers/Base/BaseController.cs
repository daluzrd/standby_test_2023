using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers.Base;
public class BaseController : Controller
{
    protected string GetToken()
    {
        var tokenExpiresIn = HttpContext.Session.GetString("ExpiresIn");
        if (tokenExpiresIn == null)
        {
            return "";
        }

        var tokenExpiresInDateTime = DateTime.Parse(tokenExpiresIn);
        if (DateTime.Now > tokenExpiresInDateTime)
        {
            HttpContext.Session.Clear();
            return "";
        }

        var token = HttpContext.Session.GetString("JwtToken");
        return token ?? "";
    }

    protected void ClearToken()
    {
        HttpContext.Session.Clear();
    }
}
