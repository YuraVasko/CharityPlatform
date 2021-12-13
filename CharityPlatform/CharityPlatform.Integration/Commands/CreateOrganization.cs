using CharityPlatform.DAL.Interfaces;
using CharityPlatform.DAL.Models;
using CharityPlatform.Domain.OrganizationConfigurationContext.Entities;
using CharityPlatform.Domain.OrganizationConfigurationContext.Enums;
using CharityPlatform.Integration.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Commands
{
    public class CreateOrganization : IRequest<Guid>
    {
        public string Name { get; }
        public string Description { get; }
        public OrganizationType OrganizationType { get; }
        public Guid Master { get; }

        public CreateOrganization(string name, string description, OrganizationType organizationType, Guid master)
        {
            Name = name;
            Description = description ;
            OrganizationType = organizationType;
            Master = master;
        }
    }

    public class CreateOrganizationHandler : IRequestHandler<CreateOrganization, Guid>
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly StoredEventSerializer _storedEventSerializer;
        private readonly IMediator _mediator;

        public CreateOrganizationHandler(IEventStoreRepository eventStore, StoredEventSerializer storedEventSerializer, IMediator mediator)
        {
            _eventStore = eventStore;
            _storedEventSerializer = storedEventSerializer;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateOrganization notification, CancellationToken cancellationToken)
        {
            var eventPossitionToAdd = 1;
            var organization = new Organization(notification.Name, notification.Description, notification.Master, notification.OrganizationType);

            var storedEvents = organization.Changes.Select(c => new StoredEvent
            {
                EventStreamId = $"Organization-{organization.Id}",
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
