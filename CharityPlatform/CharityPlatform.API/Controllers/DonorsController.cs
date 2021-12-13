using CharityPlatform.API.Models.Requests;
using CharityPlatform.API.Models.Responses;
using CharityPlatform.DAL;
using CharityPlatform.Integration.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CharityPlatform.API.Controllers
{
    [ApiController]
    [Route("api/donors")]
    public class DonorsController : ControllerBase
    {
        private readonly ILogger<DonationsController> _logger;
        private readonly IMediator _mediator;
        private readonly CharityPlatformContext _context;

        public DonorsController(ILogger<DonationsController> logger, IMediator mediator, CharityPlatformContext context)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDonor()
        {
            var userId = Guid.Parse(HttpContext.User.Identity.Name);
            var user = _context.Users.SingleOrDefault(x => x.Id == userId);

            var result = await _mediator.Send(new CreateDonor(user.FirstName, user.LastName, user.FirstName, userId));
            return Ok(new CreateDonorResponse { DonorId = result });
        }
    }
}
