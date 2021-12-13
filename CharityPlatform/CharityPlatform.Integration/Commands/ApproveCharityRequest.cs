using CharityPlatform.DAL.Interfaces;
using CharityPlatform.DAL.Models;
using CharityPlatform.Domain.CharityProjectContext.Entities;
using CharityPlatform.Integration.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Commands
{
    public class ApproveCharityRequest : IRequest<Guid>
    {
        public Guid CharityRequestId { get; set; }

        public Guid MasterId { get; set; }

        public ApproveCharityRequest(Guid charityRequestId, Guid masterId)
        {
            CharityRequestId = charityRequestId;
            MasterId = masterId;
        }
    }

    public class ApproveCharityRequestHandler : IRequestHandler<ApproveCharityRequest, Guid>
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly StoredEventSerializer _storedEventSerializer;
        private readonly IMediator _mediator;

        public ApproveCharityRequestHandler(IEventStoreRepository eventStore, StoredEventSerializer storedEventSerializer, IMediator mediator)
        {
            _eventStore = eventStore;
            _storedEventSerializer = storedEventSerializer;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(ApproveCharityRequest notification, CancellationToken cancellationToken)
        {
            var eventsModels = await _eventStore.GetCharityRequest(notification.CharityRequestId);
            var eventPossitionToAdd = eventsModels.Count + 1;
            var events = eventsModels.Select(e => _storedEventSerializer.Deserialize(e.EventJson, e.EventType)).ToArray();

            var charityRequest = new CharityProject(events);
            charityRequest.Approve();

            var storedEvents = charityRequest.Changes.Select(c => new StoredEvent
            {
                EventStreamId = $"CharityRequest-{notification.CharityRequestId}",
                EventPosition = eventPossitionToAdd++,
                EventType = c.GetType().Name,
                EventJson = _storedEventSerializer.Serialize(c)
            }).ToList();

            await _eventStore.UploadEvents(storedEvents);

            foreach (var @event in charityRequest.Changes)
            {
                await _mediator.Publish(@event);
            }

            return charityRequest.Id;
        }
    }
}
