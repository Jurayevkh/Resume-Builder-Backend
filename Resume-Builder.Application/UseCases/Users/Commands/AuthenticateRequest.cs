using MediatR;
using Resume_Builder.Domain.Entities.User;

namespace Resume_Builder.Application.UseCases.Users.Commands;

public class AuthenticateRequest:IRequest<AuthenticateResponse>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

