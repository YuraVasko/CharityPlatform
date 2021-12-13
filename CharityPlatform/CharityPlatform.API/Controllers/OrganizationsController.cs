using CharityPlatform.API.Models.Requests;
using CharityPlatform.API.Models.Responses;
using CharityPlatform.Domain.OrganizationConfigurationContext.Enums;
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
    [Route("api/charity/organizations")]
    public class OrganizationsController : ControllerBase
    {
        private readonly ILogger<OrganizationsController> _logger;
        private readonly IMediator _mediator;

        public OrganizationsController(ILogger<OrganizationsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{organizationId}")]
        public async Task<IActionResult> GetOrganization(Guid organizationId)
        {
            var result = await _mediator.Send(new GetOrganizationById { OrganizationId = organizationId });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizations()
        {
            var result = await _mediator.Send(new GetOrganizations {  });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequest request)
        {
            var result = await _mediator.Send(new CreateOrganization(request.Name, request.Description, OrganizationType.Hospital, request.Master));
            return Ok(new CreateOrganizationResponse { OrganizationId = result });
        }

        [HttpPost("master")]
        public async Task<IActionResult> AddMasterToOrganization([FromBody] AddMasterToOrganizationRequest request)
        {
            var result = await _mediator.Send(new AddMasterToOrganization(request.MasterId, request.OrganizationId));
            return Ok(new AddMasterToOrganizationResponse { OrganizationId = result });
        }

        [HttpDelete("master")]
        public async Task<IActionResult> RemoveMasterFromOrganization([FromBody] RemoveMasterFromOrganizationRequest request)
        {
            var result = await _mediator.Send(new RemoveMasterFromOrganization(request.MasterId, request.OrganizationId));
            return Ok(new RemoveMasterFromOrganizationResponse { OrganizationId = result });
        }
    }
}
