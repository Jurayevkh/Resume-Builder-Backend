using MediatR;

namespace Resume_Builder.Application.UseCases.Users.Commands;

public class DeleteUserCommand:IRequest<bool>
{
	public string Email { get; set; }
}


