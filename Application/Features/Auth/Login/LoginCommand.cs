﻿using Application.Repositories;
using Core.Entities;
using Core.Utilities;
using Core.Utilities.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Login
{
    public class LoginCommand : IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly ITokenHelper _tokenHelper;

            public LoginCommandHandler(IPatientRepository patientRepository, ITokenHelper tokenHelper)
            {
                _patientRepository = patientRepository;
                _tokenHelper = tokenHelper;
            }

            public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                Patient? patient = await _patientRepository.GetAsync(p => p.Email == request.Email);

                if (patient is null)
                {
                    throw new Exception("Giriş Başarısız");
                }

                bool isPasswordMatch = HashingHelper.VerifyPasswordHash(request.Password, patient.PasswordSalt, patient.PasswordHash);

                if(!isPasswordMatch)
                {
                    throw new Exception("Giriş Başarısız");
                }

                return _tokenHelper.CreateToken(patient);
            }
        }
    }
}
