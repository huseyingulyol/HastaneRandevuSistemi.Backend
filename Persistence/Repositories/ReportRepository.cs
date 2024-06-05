﻿using Application.Repositories;
using Core.DataAccess;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ReportRepository : EfRepositoryBase<Report, HRSDbContext>, IReportRepository
    {
        public ReportRepository(HRSDbContext context) : base(context)
        {
        }
    }

}
