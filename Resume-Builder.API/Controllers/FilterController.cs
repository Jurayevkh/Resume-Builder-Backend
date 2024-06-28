using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.API.Models;
using Resume_Builder.Application.UseCases.Filters.Queries;

namespace Resume_Builder.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FilterController:ControllerBase
{
    private readonly IMediator _mediator;

    public FilterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAllResumesByAge()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _mediator.Send(new GetAllResumesByAge())
        });

    [HttpGet]
    public async ValueTask<IActionResult> GetAllResumeByExperience()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Succes",
            Data = await _mediator.Send(new GetAllResumsByExperience())
        });

    [HttpGet]
    public async ValueTask<IActionResult> GetAllResumeByRule()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _mediator.Send(new GetAllResumesByRule())
        });

    [HttpGet]
    public async ValueTask<IActionResult> GetAllResumeByWorkingType()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _mediator.Send(new GetAllResumesByWorkingType())
        });
}

