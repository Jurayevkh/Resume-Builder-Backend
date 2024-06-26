using System.Security.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Users.Commands;

namespace Resume_Builder.Application.UseCases.Users.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
	private readonly IApplicationDbContext _applicationDbContext;

    public DeleteUserCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
		try
		{
			var user = await _applicationDbContext.Users.FirstOrDefaultAsync(user=>user.Email == request.Email);
			
			if (user is null)
				return false;
			
			_applicationDbContext.Users.Remove(user);
			var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
			return result > 0;
		}
		catch
		{
			return false;
		}
    }
}


