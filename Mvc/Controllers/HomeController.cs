using Microsoft.AspNetCore.Mvc;
using Mvc.Controllers.Base;

namespace Mvc.Controllers
{
    [Route("[controller]")]
    public class HomeController : BaseController
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Redirect("~/Account/Login");
            }

            return View();
        }
    }
}
