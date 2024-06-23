﻿using Application.Features.WorkingTimes.Commands.Create;
using Application.Features.WorkingTimes.Commands.Create;
using Application.Features.WorkingTimes.Commands.Update;
using Application.Features.WorkingTimes.Queries.GetById;
using Application.Features.WorkingTimes.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.WorkingTimes.MappingProfiles
{
    public class WorkingTimeMappingProfile : Profile
    {
        public WorkingTimeMappingProfile()
        {
            CreateMap<CreateWorkingTimeCommand, Domain.Entities.WorkingTime>().ReverseMap();
            CreateMap<CreateWorkingTimeResponse, Domain.Entities.WorkingTime>().ReverseMap();
            CreateMap<UpdateWorkingTimeCommand, Domain.Entities.WorkingTime>().ReverseMap();
            CreateMap<UpdateWorkingTimeResponse, Domain.Entities.WorkingTime>().ReverseMap();
            CreateMap<GetByIdWorkingTimeResponse, Domain.Entities.WorkingTime>().ReverseMap();
            CreateMap<GetListWorkingTimeResponse, Domain.Entities.WorkingTime>().ReverseMap();
        }
    }
}