﻿using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Utilities.Extensions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointments.Queries.GetListAppointment
{

    public class GetListAppointmentQuery : IRequest<List<GetListAppointmentResponse>>
    {
        public class GetListAppointmentQueryHandler : IRequestHandler<GetListAppointmentQuery, List<GetListAppointmentResponse>>, ISecuredRequest
        {

            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public GetListAppointmentQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor contextAccessor)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
                _httpContextAccessor = contextAccessor;
            }

            public string[] RequiredRoles => ["Patient"];


            public async Task<List<GetListAppointmentResponse>> Handle(GetListAppointmentQuery request, CancellationToken cancellationToken)
            {

                int userId = _httpContextAccessor.HttpContext.User.GetUserId();

                List<Appointment> allAppointments = (await _appointmentRepository.GetListAsync(a => a.PatientId == userId,
                    include: a => a.Include(a => a.Doctor).ThenInclude(d => d.User)
                    .Include(u => u.Doctor).ThenInclude(d => d.Clinic)
                    .Include(a => a.Doctor).ThenInclude(d => d.OfficeLocation).ThenInclude(u => u.Block)
                    .Include(a => a.Doctor).ThenInclude(d => d.OfficeLocation).ThenInclude(o => o.Floor)
                    .Include(a => a.Doctor).ThenInclude(d => d.OfficeLocation).ThenInclude(u => u.Room)
                    ));

                var sortedAppointments = allAppointments
                    .OrderBy(a => a.DateTime > DateTime.Now ? 0 : 1) // Önce aktif randevular
                    .ThenBy(a => a.Status != AppointmentStatus.Scheduled ? 1 : 0) // Sonra iptal edilmemiş randevular
                    .ThenBy(a => a.DateTime > DateTime.Now ? a.DateTime : DateTime.MaxValue) // Aktif randevular için küçükten büyüğe
                    .ThenByDescending(a => a.DateTime <= DateTime.Now ? a.DateTime : DateTime.MinValue) // Geçmiş randevular için büyükten küçüğe
                    .ToList();


                List<GetListAppointmentResponse> response = _mapper.Map<List<GetListAppointmentResponse>>(sortedAppointments);
                return response;
            }
        }
    }
}
