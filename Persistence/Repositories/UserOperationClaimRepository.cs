﻿using Application.Repositories;
using Core.DataAccess;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Persistence.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, HRSDbContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository (HRSDbContext context) : base(context)
        {
        }
    }
}
