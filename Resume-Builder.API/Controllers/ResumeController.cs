using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.API.Helpers;
using Resume_Builder.Application.DTO;
using Resume_Builder.Application.UseCases.Resumes.Commands;
using Resume_Builder.Application.UseCases.Resumes.Queries;

namespace Resume_Builder.API;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class ResumeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ResumeController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAllResumes()
    {
        var resumes = _mediator.Send(new GetAllResumesQuery());
        return Ok(await resumes);
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetResumeById(int Id)
    {
        var resume = _mediator.Send(new GetResumeByIdQuery() { Id = Id });
        return Ok(await resume);
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateResume(CreateResumeDTO dto)
    {
        var resume = _mapper.Map<CreateResumeCommand>(dto);
        var result = await _mediator.Send(resume);
        return Ok(result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateResume(UpdateResumeDTO dto)
    {
        var resume = _mapper.Map<UpdateResumeCommand>(dto);
        var result = await _mediator.Send(resume);
        return Ok(result);
    }

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteResume(int Id)
    {
        var result = _mediator.Send(new DeleteResumeCommand() { Id = Id });
        return Ok(await result);
    }
}
