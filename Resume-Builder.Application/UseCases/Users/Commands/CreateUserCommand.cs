using MediatR;
using Resume_Builder.Domain.Entities.User;

namespace Resume_Builder.Application.UseCases.Users.Commands;

public class CreateUserCommand:IRequest<AuthenticateResponse>
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
}


