﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Commands.Create
{
    public class CreateReportResponse
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        public string Description { get; set; }
    }
}