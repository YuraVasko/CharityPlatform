using CharityPlatform.API.Models.Requests;
using CharityPlatform.API.Models.Responses;
using CharityPlatform.Integration.Commands;
using CharityPlatform.Integration.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CharityPlatform.API.Controllers
{
    [ApiController]
    [Route("api/charity/projects")]
    public class CharityProjectsController : ControllerBase
    {
        private readonly ILogger<CharityProjectsController> _logger;
        private readonly IMediator _mediator;

        public CharityProjectsController(ILogger<CharityProjectsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharityProject([FromBody] CreateCharityProjectRequest request)
        {
            var result = await _mediator.Send(new CreateCharityRequest(request.Title,
                                                                       request.Description,
                                                                       request.CharityOrganizationId,
                                                                       request.DonationGoal,
                                                                       request.StartDate,
                                                                       request.EndDate,
                                                                       Guid.NewGuid()));
            return Ok(new CreateCharityProjectResponse { CharityRequestId = result });
        }


        [HttpGet("organization/{organizationId}")]
        public async Task<IActionResult> GetOrganizationCharityProjects(Guid organizationId)
        {
            var result = await _mediator.Send(new GetCharityProjectsByOrganizationId { OrganizationId = organizationId });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCharityProjects()
        {
            var result = await _mediator.Send(new GetCharityProjects {});
            return Ok(result);
        }

        [HttpPost("{charityProjectId}/approve")]
        public async Task<IActionResult> ApproveOrganizationCharityProject([FromRoute] Guid charityProjectId)
        {
            var result = await _mediator.Send(new ApproveCharityRequest(charityProjectId, Guid.NewGuid()));
            return Ok(new CloseCharityProjectResponse { CharityRequestId = result });
        }


        [HttpPost("{charityProjectId}/reject")]
        public async Task<IActionResult> RejectOrganizationCharityProject([FromRoute] Guid charityProjectId)
        {
            var result = await _mediator.Send(new RejectCharityRequest(charityProjectId, Guid.NewGuid()));
            return Ok(new CloseCharityProjectResponse { CharityRequestId = result });
        }

        [HttpDelete]
        public async Task<IActionResult> CloseCharityProject([FromBody] CloseCharityProjectRequest request)
        {
            var result = await _mediator.Send(new CloseCharityRequest(request.CharityRequestId, Guid.NewGuid()));
            return Ok(new CloseCharityProjectResponse { CharityRequestId = result });
        }
    }
}
