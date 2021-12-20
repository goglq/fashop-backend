namespace FashopBackend.Core.Aggregate.TokenAggregate;

public class Tokens
{
    public string AccessToken { get; }
    
    public string RefreshToken { get; }

    public Tokens(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}