using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Resumes.Commands;

namespace Resume_Builder.Application.UseCases.Resumes.Handlers;

public class DeleteResumeCommandHandler : IRequestHandler<DeleteResumeCommand, bool>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DeleteResumeCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var resume = await _applicationDbContext.Resumes.FirstOrDefaultAsync(resume=>resume.Id==request.Id);

            if (resume is null)
                return false;

            _applicationDbContext.Resumes.Remove(resume);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return result > 0;

        }
        catch
        {
            return false;
        }
    }
}

