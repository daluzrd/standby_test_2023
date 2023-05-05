namespace Mvc.Models.Account;

public class JwtToken
{
    public string AccessToken { get; private set; }
    public DateTime ExpiresIn { get; private set; }

    public JwtToken(string accessToken, DateTime expiresIn)
    {
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
    }
}