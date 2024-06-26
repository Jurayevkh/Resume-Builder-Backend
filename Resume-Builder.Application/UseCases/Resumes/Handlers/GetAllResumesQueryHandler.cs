namespace Resume_Builder.Application.UseCases.Resumes.Handlers;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Resumes.Queries;
using Resume_Builder.Domain.Entities.Resume;

public class GetAllResumesQueryHandler : IRequestHandler<GetAllResumesQuery, List<Resumes>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public GetAllResumesQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Resumes>> Handle(GetAllResumesQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Resumes.ToListAsync(cancellationToken);
    }
}

