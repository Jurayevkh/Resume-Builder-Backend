using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.API.Helpers;
using Resume_Builder.API.Models;
using Resume_Builder.Application.DTO;
using Resume_Builder.Application.UseCases.Users.Commands;
using Resume_Builder.Application.UseCases.Users.Queries;

namespace Resume_Builder.API;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class UserController:ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

   [HttpGet]
    public async ValueTask<IActionResult> GetAllUsers()
    => Ok(new Response
    {
        StatusCode=200,
        Message="Succes",
        Data=await _mediator.Send(new GetAllUsersQuery())
    });

    [HttpGet]
    public async ValueTask<IActionResult> GetUserById(int Id)
    => Ok(new Response
    {
        StatusCode=200,
        Message="Succes",
        Data=await _mediator.Send(new GetUserByIdQuery(){Id=Id})
    });
    

    [HttpPut]
    public async ValueTask<IActionResult> UpdateUser(UpdateUserDTO dto)
    => Ok(new Response
    {
        StatusCode=200,
        Message="Success",
        Data=await _mediator.Send(_mapper.Map<UpdateUserCommand>(dto))
    });

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteUser(string Email)
    => Ok(new Response
    {
        StatusCode=200,
        Message="Success",
        Data=await _mediator.Send(new DeleteUserCommand() { Email = Email })
    });
    

}
