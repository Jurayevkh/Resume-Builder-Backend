using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.API.Models;
using Resume_Builder.Application.DTO;
using Resume_Builder.Application.UseCases.Users.Commands;

namespace Resume_Builder.API;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController:ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async ValueTask<IActionResult> Login(AuthenticateRequest authenticateRequest)
    {
        var response = await _mediator.Send(authenticateRequest);

        if (response is null) return BadRequest(new {message="Email or password is incorrect"});

        return Ok(response);
    }

    [HttpPost]
    public async ValueTask<IActionResult> Register(RegisterUserDTO dto)
    => Ok(new Response
    {
        StatusCode=200,
        Message="Success",
        Data=await _mediator.Send(_mapper.Map<CreateUserCommand>(dto))
    });

}
