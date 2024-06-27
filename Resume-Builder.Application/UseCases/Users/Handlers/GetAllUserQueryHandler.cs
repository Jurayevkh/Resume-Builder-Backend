namespace Resume_Builder.Application.UseCases.Users.Handlers;

using System.Threading;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Users.Queries;
using Resume_Builder.Domain.Entities.User;

public class GetAllUserQueryHandler:IRequestHandler<GetAllUsersQuery, List<Users>>
{
	private readonly IApplicationDbContext _applicationDbContext;

    public GetAllUserQueryHandler(IApplicationDbContext applicationDbContext)
   {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Users>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
		return await _applicationDbContext.Users.ToListAsync();
    }
}


