﻿using Application.Features.Appointments.Commands.Book;
using Application.Features.Appointments.Queries.GetListActiveAppointment;
using Application.Features.Appointments.Queries.GetListAppointment;
using Application.Features.Appointments.Queries.GetListAvailableAppointment;
using Application.Features.Appointments.Queries.GetListPastAppointmentByDoctor;
using Application.Features.Appointments.Queries.GetListPatientByDoctor;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Appointments.Commands.Cancel.ByDoctor;
using Application.Features.Appointments.Commands.Cancel.ByPatient;
using Application.Features.Appointments.Queries.GetListAppointmentByAdmin;

namespace WebAPI.Controllers
{
    public class AppointmentController : BaseController
    {


        [HttpPost("Book")]
        public async Task<IActionResult> Book([FromBody] BookAppointmentCommand command)
        {
            BookAppointmentResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("CancelByDoctor/{id}")]
        public async Task<IActionResult> CancelByDoctor([FromRoute] int id)
        {
            CancelAppointmentByDoctorCommand command = new() { Id = id };
            CancelAppointmentByDoctorResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("CancelByPatient/{id}")]
        public async Task<IActionResult> CancelByPatient([FromRoute] CancelAppointmentByPatientCommand command)
        {
            CancelAppointmentByPatientResponse response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet("List")]
        public async Task<IActionResult> GetList([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return await PagedQuery<GetListAppointmentQuery, GetListAppointmentResponse>(page, pageSize);
        }

        [HttpGet("GetListAvailableAppointments")]
        public async Task<IActionResult> GetListAvailable([FromQuery] GetListAvailableAppointmentQuery query)
        {
            GetListAvailableAppointmentResponse response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetListActiveAppointmentByDoctor")]
        public async Task<IActionResult> GetListActive()
        {
            GetListActiveAppointmentByDoctorQuery query = new();
            List<GetListActiveAppointmentByDoctorResponse> response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetListPastAppointmentByDoctor")]
        public async Task<IActionResult> GetListPast([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return await PagedQuery<GetListPastAppointmentByDoctorQuery, GetListPastAppointmentByDoctorResponse>(page, pageSize);
        }

        [HttpGet("GetListPatientByDoctor")]
        public async Task<IActionResult> GetListPatient([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return await PagedQuery<GetListPatientByDoctorQuery, GetListPatientByDoctorResponse>(page, pageSize);
        }

        [HttpGet("GetListAppointmentByAdmin")]
        public async Task<IActionResult> GetListAppointment([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return await PagedQuery<GetListAppointmentByAdminQuery, GetListAppointmentByAdminResponse>(page, pageSize);
        }
    }
}
