using System.IdentityModel.Tokens.Jwt;

internal static class Extensions
{
    public static string GetUserIdFromToken(this HttpContext httpContext)
    {
        var authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return null;
        }

        var token = authorizationHeader.Substring("Bearer ".Length);

        var handler = new JwtSecurityTokenHandler();
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;

        var userId = tokenS?.Claims?.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        return userId;
    }
}