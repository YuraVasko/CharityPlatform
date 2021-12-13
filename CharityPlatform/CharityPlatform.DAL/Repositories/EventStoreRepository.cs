using CharityPlatform.DAL.Interfaces;
using CharityPlatform.DAL.Models;
using CharityPlatform.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityPlatform.DAL.Repositories
{
    class EventStoreRepository : IEventStoreRepository
    {
        private readonly CharityPlatformContext _context;
        private readonly IMediator _mediator;

        public EventStoreRepository(CharityPlatformContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public Task<List<StoredEvent>> GetCharityRequest(Guid donationRequestId)
        {
            return _context.StoredEvents.Where(e => e.EventStreamId == $"CharityRequest-{donationRequestId}")
                            .OrderBy(e => e.EventPosition)
                            .ToListAsync();
        }

        public Task<List<StoredEvent>> GetDonorEvents(Guid donorId)
        {
            return _context.StoredEvents.Where(e => e.EventStreamId == $"Donor-{donorId}")
                                        .OrderBy(e => e.EventPosition)
                                        .ToListAsync();
        }

        public Task<List<StoredEvent>> GetOrganization(Guid organizationId)
        {
            return _context.StoredEvents.Where(e => e.EventStreamId == $"Organization-{organizationId}")
                                        .OrderBy(e => e.EventPosition)
                                        .ToListAsync();
        }

        public async Task UploadEvents(List<StoredEvent> events)
        {
            _context.StoredEvents.AddRange(events);
            await _context.SaveChangesAsync();
        }
    }
}
