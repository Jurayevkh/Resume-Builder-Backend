using MediatR;

namespace Resume_Builder.Application.UseCases.Users.Commands;

public class CreateUserCommand:IRequest<bool>
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
}


