using MediatR;

namespace Resume_Builder.Application.UseCases.Resumes.Commands;

public class DeleteResumeCommand:IRequest<bool>
{
    public int Id { get; set; }
}

