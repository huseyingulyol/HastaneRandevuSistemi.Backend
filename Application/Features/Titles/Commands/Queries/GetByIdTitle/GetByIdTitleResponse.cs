﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Titles.Commands.Queries.GetByIdTitle
{
    public class GetByIdTitleResponse
    {
        public Title? Title { get; set; }
    }
}