using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Users.Commands;

namespace Resume_Builder.Application.UseCases.Users.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
	private readonly IApplicationDbContext _applicationDbContext;

    public UpdateUserCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
		try
		{
			var user = await _applicationDbContext.Users.FirstOrDefaultAsync(user=>user.Id == request.Id);
			if (user is null)
				return false;
			user.Email = request.Email ?? user.Email;
			user.FirstName = request.FirstName ?? user.FirstName;
			user.LastName = request.LastName ?? user.LastName;
			user.Password = request.Password ?? user.Password;

			_applicationDbContext.Users.AddAsync(user);
			var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
			return result>0;
		}
		catch
		{
			return false;
		}
    }
}


