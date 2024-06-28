namespace Resume_Builder.Application.UseCases.Users.Handlers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Users.Commands;
using Resume_Builder.Domain.Auth;
using Resume_Builder.Domain.Entities.User;

//"Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: MediatR.IRequestHandler`2[Resume_Builder.Application.UseCases.Users.Commands.AuthenticateRequest,Resume_Builder.Domain.Entities.User.AuthenticateResponse] Lifetime: Transient ImplementationType: Resume_Builder.Application.UseCases.Users.Handlers.Authenticate': Unable to resolve service for type 'Resume_Builder.Domain.Auth.AppSettings' while attempting to activate 'Resume_Builder.Application.UseCases.Users.Handlers.Authenticate'.)"
public class Authenticate : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
{
    private readonly AppSettings _appSettings;
    private readonly IApplicationDbContext _applicationDbContext;

    public Authenticate(IApplicationDbContext applicationDbContext, IOptions<AppSettings> appSettings)
    {
        _applicationDbContext = applicationDbContext;
        _appSettings = appSettings.Value;
    }


    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext.Users.SingleOrDefaultAsync(user=>user.Email==request.Email && user.Password==request.Password);

        if (user is null) return null;

        var token = await GenerateToken(user);
        return new AuthenticateResponse(user, token);
    }

    private async Task<string> GenerateToken(Users user)
    {
        var identityClaims = new Claim[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim("Email", user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: identityClaims,
            expires:DateTime.UtcNow.AddMinutes(10),
            signingCredentials: keyCredentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
        //var tokenHandler = new JwtSecurityTokenHandler();
        //var token = await Task.Run(() =>
        //{
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        //        Expires = DateTime.UtcNow.AddMinutes(10),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    return tokenHandler.CreateToken(tokenDescriptor);
        //});
        //return tokenHandler.WriteToken(token);
    }


}

