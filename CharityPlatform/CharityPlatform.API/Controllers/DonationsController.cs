using CharityPlatform.API.Models.Requests;
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
    [Route("api/donations")]
    public class DonationsController : ControllerBase
    {
        private readonly ILogger<DonationsController> _logger;
        private readonly IMediator _mediator;

        public DonationsController(ILogger<DonationsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDonorDonations()
        {
            var result = await _mediator.Send(new GetDonorDonations { DonorId = Guid.NewGuid()});
            return Ok(result);
        }

        [HttpGet("project/{charityRequestId}")]
        public async Task<IActionResult> GetDonorDonationsByRequest([FromRoute] Guid charityRequestId)
        {
            var result = await _mediator.Send(new GetDonationsByProjectId { CharityProjectId = charityRequestId });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDonation([FromBody] AddDonationRequest request)
        {
            var result = await _mediator.Send(new AddDonation(request.Amount, request.DonorId, request.CharityRequestId));
            return Ok(result);
        }
        
    }
}
