﻿using Application.Features.Appointments.Commands.Create;
using Application.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Utilities;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Register
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPatientRepository _patientRepository;

            public RegisterCommandHandler(IMapper mapper, IPatientRepository patientRepository)
            {
                _mapper = mapper;
                _patientRepository = patientRepository;
            }
            public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {


                Patient? patientWithSameEmail = await _patientRepository.GetAsync(p => p.Email == request.Email);
                if (patientWithSameEmail is not null)
                {
                    throw new BusinessException("Email adresi ile daha önceden sisteme kayıt yapılmış");
                }

                Patient newPatient = _mapper.Map<Patient>(request);

                byte[] passwordSalt, passwordHash;

                HashingHelper.CreatePasswordHash(request.Password, out passwordSalt, out passwordHash);
                newPatient.PasswordSalt = passwordSalt;
                newPatient.PasswordHash = passwordHash;
                newPatient.UserOperationClaims = new List<UserOperationClaim>() {
                    new UserOperationClaim() { OperationClaimId = 1, UserId = newPatient.Id }
                };
                await _patientRepository.AddAsync(newPatient);

                return new RegisterResponse();
            }
        }


        

    }

    
}
