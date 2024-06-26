namespace Resume_Builder.Application.UseCases.Resumes.Queries;
using MediatR;
using Resume_Builder.Domain.Entities.Resume;

public class GetResumeByIdQuery:IRequest<Resumes>
{
    public int Id { get; set; }
}

