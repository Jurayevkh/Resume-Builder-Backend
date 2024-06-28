namespace Resume_Builder.Application.UseCases.Filters.Handlers;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Filters.Queries;
using Resume_Builder.Domain.Entities.Resume;

public class GetAllResumesByRuleHandler : IRequestHandler<GetAllResumesByRule, List<Resumes>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetAllResumesByRuleHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Resumes>> Handle(GetAllResumesByRule request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Resumes.OrderBy(resume => resume.Rule).ToListAsync();
    }
}

