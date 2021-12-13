using CharityPlatform.DAL.Interfaces;
using CharityPlatform.DAL.Models;
using CharityPlatform.Domain.OrganizationConfigurationContext.Entities;
using CharityPlatform.Integration.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Commands
{
    public class RemoveMasterFromOrganization : IRequest<Guid>
    {
        public Guid MasterId { get; }

        public Guid OrganizationId { get; }

        public RemoveMasterFromOrganization(Guid masterId, Guid organizationId)
        {
            MasterId = masterId;
            OrganizationId = organizationId;
        }
    }

    public class RemoveMasterFromOrganizationHandler : IRequestHandler<RemoveMasterFromOrganization, Guid>
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly StoredEventSerializer _storedEventSerializer;
        private readonly IMediator _mediator;

        public RemoveMasterFromOrganizationHandler(IEventStoreRepository eventStore, StoredEventSerializer storedEventSerializer, IMediator mediator)
        {
            _eventStore = eventStore;
            _storedEventSerializer = storedEventSerializer;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(RemoveMasterFromOrganization notification, CancellationToken cancellationToken)
        {
            var eventsModels = await _eventStore.GetOrganization(notification.OrganizationId);
            var eventPossitionToAdd = eventsModels.Count + 1;
            var events = eventsModels.Select(e => _storedEventSerializer.Deserialize(e.EventJson, e.EventType)).ToArray();

            var organization = new Organization(events);
            organization.RemoveMaster(notification.MasterId);

            var storedEvents = organization.Changes.Select(c => new StoredEvent
            {
                EventStreamId = $"Organization-{notification.OrganizationId}",
                EventPosition = eventPossitionToAdd++,
                EventType = c.GetType().Name,
                EventJson = _storedEventSerializer.Serialize(c)
            }).ToList();

            await _eventStore.UploadEvents(storedEvents);

            foreach (var @event in organization.Changes)
            {
                await _mediator.Publish(@event);
            }

            return organization.Id;
        }
    }
}
