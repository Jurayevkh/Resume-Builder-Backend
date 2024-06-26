namespace Resume_Builder.Application.UseCases.Resumes.Queries;
using MediatR;

using Resume_Builder.Domain.Entities.Resume;

public class GetAllResumesQuery:IRequest<List<Resumes>>
{

}

