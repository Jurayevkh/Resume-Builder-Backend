using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Resume_Builder.Application.Abstractions;
using Resume_Builder.Application.UseCases.Resumes.Commands;

namespace Resume_Builder.Application.UseCases.Resumes.Handlers;

public class UpdateResumeCommandHandler : IRequestHandler<UpdateResumeCommand, bool>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UpdateResumeCommandHandler(IApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _applicationDbContext = applicationDbContext;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<bool> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var resume = await _applicationDbContext.Resumes.FirstOrDefaultAsync(resume=>resume.Id==request.Id);
            if (resume is null)
                return false;

            resume.FullName = request.FirstName + " " + request.LastName;
            resume.Age = request.Age ?? resume.Age; 
            resume.PhoneNumber = request.PhoneNumber ?? resume.PhoneNumber;
            resume.SocialMediaUserName = request.SocialMediaUserName ?? resume.SocialMediaUserName;
            resume.Rule = request.Rule ?? resume.Rule;
            resume.Experience = request.Experience ?? resume.Experience;
            resume.Salary = request.Salary ?? resume.Salary;
            resume.WorkingType = request.WorkingType ?? resume.WorkingType;
            resume.Skills = request.Skills ?? resume.Skills;
            resume.Languages = request.Languages ?? resume.Languages;
            resume.Projects = request.Projects ?? resume.Projects;
            resume.Portfolio = request.Portfolio ?? resume.Portfolio;
            resume.Github = request.Github ?? resume.Github;
            resume.Linkedin = request.Linkedin ?? resume.Linkedin;

            string uniqueFileName = string.Empty;
            if (request.Photo != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Photo.FileName;
                string imageFilePath = Path.Combine(uploadFolder, uniqueFileName);
                request.Photo.CopyTo(new FileStream(imageFilePath, FileMode.Create));
                if (resume.Photo != null)
                {
                    string wwwRootPath = "/Users/mac/Desktop/Resume-Builder-Backend/Resume-Builder.API/wwwroot";
                    string imagesFolderPath = Path.Combine(wwwRootPath, resume.Photo);
                    File.Delete(imagesFolderPath);
                }
                resume.Photo = "images/" + uniqueFileName;
            }

            _applicationDbContext.Resumes.Update(resume);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        catch
        {
            return false;
        }
    }
}

