namespace Resume_Builder.Application.UseCases.Users.Queries;
using MediatR;
using Resume_Builder.Domain.Entities.User;

public class GetUserByIdQuery:IRequest<Users>
{
	public int Id{get;set;}
}


