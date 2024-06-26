namespace Resume_Builder.Application.UseCases.Resumes.Handlers;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Resumes.Queries;
using Resume_Builder.Domain.Entities.Resume;

public class GetResumeByIdQueryHandler : IRequestHandler<GetResumeByIdQuery, Resumes>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetResumeByIdQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Resumes> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
    {
        var resume = await _applicationDbContext.Resumes.FirstOrDefaultAsync(resume=>resume.Id==request.Id);
        return resume;
    }
}

