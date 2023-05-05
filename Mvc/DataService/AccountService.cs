using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Mvc.DataService.Interface;
using Mvc.Models.Account;
using Newtonsoft.Json;

namespace Mvc.DataService;

public class AccountService : IAccountService
{

    public async Task<JwtToken> Login(LoginViewModel loginViewModel)
    {
        using (var client = new HttpClient())
        {
            var jsonContent = JsonConvert.SerializeObject(loginViewModel);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(@"http://localhost:5000/api/Account/Login", contentString);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception(response.Content.ToString());
                }

                throw new Exception("Ocorreu um erro realizar o login.");
            }

            var jwtToken = await response.Content.ReadFromJsonAsync<JwtToken>();
            if (jwtToken == null)
            {
                throw new Exception("Ocorreu um erro realizar o login.");
            }

            return jwtToken;
        }
        
        throw new Exception("Ocorreu um erro realizar o login.");
    }

    public async Task Register(RegisterViewModel registerViewModel)
    {
        using (var client = new HttpClient())
        {
            var jsonContent = JsonConvert.SerializeObject(registerViewModel);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(@"http://localhost:5000/api/Account/Register", contentString);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ToString());
            }
        }
    }
}