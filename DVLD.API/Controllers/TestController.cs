﻿using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.TestDTOs;
using DVLD.Application.DTO.Users;
using DVLD.Application.Features.TestFeatuer.Commandes.AddTestAppointment;
using DVLD.Application.Features.TestFeatuer.Commandes.AddTestResult;
using DVLD.Application.Features.TestFeatuer.Queries.GetScheduleTestInfoAsync;
using DVLD.Application.Features.TestFeatuer.Queries.GetTestAppointmentDetailInfo;
using DVLD.Domain.views.Test;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers
{
    [ApiController]
    public class TestController : AppController
    {
        public TestController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet(Router.TestRouting.GetTestLocalDrivingLicenseDetail)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<TestAppointmentDetailInfo>>> GetTestLocalDrivingLicenseDetail([FromRoute] int localDrivingApplication)
        {
            var response = await _mediator.Send(new GetTestLocalDrivingLicenseApplicationDetailQuery(localDrivingApplication));
            return NewResult(response);
        }

        [HttpGet(Router.TestRouting.GetTestAppoiments)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<IEnumerable<TestAppointmentView>>>> GetTestAppoiments([FromQuery] int localDrivingApplication, [FromQuery] int TestTypeId)
        {
            var response = await _mediator.Send(new GetTestAppointmentsQuery(localDrivingApplication, TestTypeId));
            return NewResult(response);
        }



        [HttpGet(Router.TestRouting.GetScheduleTestInfoView)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<GetUserDTO>>> GetScheduleTestInfo([FromQuery] int LocalDrivingApplicationId, [FromQuery] int TestTypeId)
        {
            var response = await _mediator.Send(new GetScheduleTestInfoQuery(LocalDrivingApplicationId, TestTypeId));
            return NewResult(response);
        }

        [HttpPost(Router.TestRouting.AddTestAppointment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<GetUserDTO>>> AddTestAppoimentInfoView([FromBody] AddTestAppintmentDTO AddTestAppintmentDTO)
        {
            var response = await _mediator.Send(new AddTestAppointmentCommand(AddTestAppintmentDTO));
            return NewResult(response);
        }

        [HttpPost(Router.TestRouting.AddTestResult)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<GetUserDTO>>> AddTestResult([FromBody] AddTestDTO AddTestDTO)
        {
            var response = await _mediator.Send(new AddTestResultCommand(AddTestDTO));
            return NewResult(response);
        }
    }
}
