namespace Resume_Builder.API.Helpers;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Domain.Auth;

public class JWTMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JWTMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IApplicationDbContext _applicationDbContext)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            await attachUserToContext(context, _applicationDbContext, token);

        await _next(context);
    }

    private async Task attachUserToContext(HttpContext context, IApplicationDbContext _applicationDbContext, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            //Attach user to context on successful JWT validation
            context.Items["User"] = await _applicationDbContext.Users.FirstOrDefaultAsync(user=>user.Id==userId);
        }
        catch
        {
            //Do nothing if JWT validation fails
            // user is not attached to context so the request won't have access to secure routes
        }
    }
}
