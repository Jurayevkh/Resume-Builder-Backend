using AutoMapper;
using Resume_Builder.Application.DTO;
using Resume_Builder.Application.UseCases.Resumes.Commands;
using Resume_Builder.Application.UseCases.Users.Commands;


namespace Resume_Builder.Application;

public class MappingConfiguration:Profile
{
    public MappingConfiguration(){
        CreateMap<CreateUserCommand , RegisterUserDTO>().ReverseMap();
        CreateMap<UpdateUserCommand , UpdateUserDTO>().ReverseMap();

        CreateMap<CreateResumeCommand, CreateResumeDTO>().ReverseMap();
        CreateMap<UpdateResumeCommand, UpdateResumeDTO>().ReverseMap();
    }
}
