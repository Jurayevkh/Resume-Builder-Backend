﻿namespace Resume_Builder.Application.UseCases.Filters.Queries;

using MediatR;
using Resume_Builder.Domain.Entities.Resume;

public class GetAllResumesByWorkingType:IRequest<List<Resumes>>
{

}

