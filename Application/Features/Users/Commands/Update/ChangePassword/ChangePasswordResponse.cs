﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Update.ChangePassword
{
    public class ChangePasswordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}