namespace Resume_Builder.Application.UseCases.Users.Handlers;
using MediatR;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Users.Commands;
using Resume_Builder.Domain.Entities.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
	private readonly IApplicationDbContext _applicationDbContext;

    public CreateUserCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
			return result>0;
		}
		catch
		{
			return false;
		}
    }
}


