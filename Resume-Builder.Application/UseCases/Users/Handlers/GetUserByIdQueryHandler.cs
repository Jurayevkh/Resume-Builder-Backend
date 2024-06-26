namespace Resume_Builder.Application.UseCases.Users.Handlers;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Users.Queries;
using Resume_Builder.Domain.Entities.User;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Users>
{
	private readonly IApplicationDbContext _applicationDbContext;

    public GetUserByIdQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Users> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
		return await _applicationDbContext.Users.FirstOrDefaultAsync(user=>user.Id == request.Id);
    }
}


