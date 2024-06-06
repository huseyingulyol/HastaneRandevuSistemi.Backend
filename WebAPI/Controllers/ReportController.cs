﻿using Application.Features.Reports.Commands.Create;
using Application.Features.Reports.Queries.GetById;
using Application.Features.Reports.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateReportCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] CreateReportCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CreateReportCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdQuery getByIdReportQuery)
        {
            GetByIdReportResponse result = await _mediator.Send(getByIdReportQuery);
            return Ok(result);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            GetListQuery getListQuery = new GetListQuery();
            List<GetListReportResponse> result = await _mediator.Send(getListQuery);

            return Ok(result);
        }
    }
}
