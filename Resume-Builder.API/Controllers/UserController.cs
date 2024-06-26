using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.Application.DTO;
using Resume_Builder.Application.UseCases.Users.Commands;
using Resume_Builder.Application.UseCases.Users.Queries;

namespace Resume_Builder.API;

[Route("api/[controller]/[action]")]
[ApiController]
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
    {
        var users = _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetUserById(int Id)
    {
        var user = _mediator.Send(new GetUserByIdQuery() { Id = Id });
        return Ok(user);
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateUser(CreateUserDTO dto)
    {
        var user = _mapper.Map<CreateUserCommand>(dto);
        var result = await _mediator.Send(user);
        return Ok(result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateUser(UpdateUserDTO dto)
    {
        var user = _mapper.Map<UpdateUserCommand>(dto);
        var result = await _mediator.Send(user);
        return Ok(result);
    }

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteUser(string Email)
    {
        var result = _mediator.Send(new DeleteUserCommand() { Email = Email });
        return Ok(result);
    }

}
