namespace Resume_Builder.Application.UseCases.Resumes.Handlers;
using MediatR;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Resumes.Commands;
using Resume_Builder.Domain.Entities.Resume;

public class CreateResumeCommandHandler : IRequestHandler<CreateResumeCommand, bool>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public CreateResumeCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> Handle(CreateResumeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Resumes resume = new Resumes
            {
                FullName = request.FirstName + " " + request.LastName,
                Age = request.Age,
                PhoneNumber = request.PhoneNumber,
                SocialMediaUserName = request.SocialMediaUserName,
                Rule = request.Rule,
                Experience = request.Experience,
                Salary = request.Salary,
                WorkingType = request.WorkingType,
                Skills = request.Skills,
                Languages = request.Languages,
                Projects = request.Projects,
                Photo = request.Photo,
                Portfolio = request.Portfolio,
                Github = request.Github,
                Linkedin = request.Linkedin,
                CreatedAt = DateTime.UtcNow
            };

            await _applicationDbContext.Resumes.AddAsync(resume);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        catch
        {
            return false;
        }

            
    }
}

