﻿using FluentValidation;

namespace Application.Features.Clinics.Commands.Create
{
    public class CreateClinicCommandValidator : AbstractValidator<CreateClinicCommand>
    {

        public CreateClinicCommandValidator() 
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Klinik alanı boş bırakılamaz.");
            RuleFor(i => i.PhoneNumber).NotEmpty().WithMessage("Telefon alanı boş bırakılamaz.");
            RuleFor(i => i.AppointmentDuration).NotEmpty().WithMessage("Randevu süresi alanı boş bırakılamaz.");
        }
    }
}
