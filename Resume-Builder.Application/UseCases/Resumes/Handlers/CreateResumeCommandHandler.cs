namespace Resume_Builder.Application.UseCases.Resumes.Handlers;

using MediatR;
using Microsoft.AspNetCore.Hosting;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Resumes.Commands;
using Resume_Builder.Domain.Entities.Resume;

public class CreateResumeCommandHandler : IRequestHandler<CreateResumeCommand, bool>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateResumeCommandHandler(IApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _applicationDbContext = applicationDbContext;
        _webHostEnvironment = webHostEnvironment;
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
                Portfolio = request.Portfolio,
                Github = request.Github,
                Linkedin = request.Linkedin,
                CreatedAt = DateTime.UtcNow
            };
            string uniqueFileName = string.Empty;
            if(request.Photo != null)
            {
                string UploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Photo.FileName;
                string imageFilePath = Path.Combine(UploadFolder,uniqueFileName);
                request.Photo.CopyTo(new FileStream(imageFilePath,FileMode.Create));
                resume.Photo = "images/" + uniqueFileName;
            }

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

