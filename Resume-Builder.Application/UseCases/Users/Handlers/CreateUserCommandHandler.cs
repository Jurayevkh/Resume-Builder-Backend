namespace Resume_Builder.Application.UseCases.Users.Handlers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Users.Commands;
using Resume_Builder.Domain.Auth;
using Resume_Builder.Domain.Entities.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthenticateResponse>
{
	private readonly IApplicationDbContext _applicationDbContext;
    private readonly AppSettings _appSettings;

    public CreateUserCommandHandler(IApplicationDbContext applicationDbContext, IOptions<AppSettings> appSettings)
    {
        _applicationDbContext = applicationDbContext;
        _appSettings = appSettings.Value;
    }

    public async Task<AuthenticateResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
		try
		{
			Users user= new Users(){
				FirstName=request.FirstName,
				LastName=request.LastName,
				Email=request.Email,
				Password=request.Password,
			};
			_applicationDbContext.Users.AddAsync(user);
			var result=await _applicationDbContext.SaveChangesAsync(cancellationToken);
            var token = await GenerateToken(user);
            return new AuthenticateResponse(user, token);
		}
		catch
		{
			return null;
		}
    }

    private async Task<string> GenerateToken(Users user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });
        return tokenHandler.WriteToken(token);
    }
}


