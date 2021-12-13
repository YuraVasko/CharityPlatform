using CharityPlatform.DAL.Interfaces;
using CharityPlatform.DAL.Models;
using CharityPlatform.Domain.CharityDonorsContext.Entities;
using CharityPlatform.Integration.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Commands
{
    public class UpgradeDonorState : IRequest<Guid>
    {
        public Guid DonorId { get; }

        public UpgradeDonorState(Guid donorId)
        {
            DonorId = donorId;
        }
    }

    public class UpgradeDonorStateHandler : IRequestHandler<UpgradeDonorState, Guid>
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly StoredEventSerializer _storedEventSerializer;
        private readonly IMediator _mediator;

        public UpgradeDonorStateHandler(IEventStoreRepository eventStore, StoredEventSerializer storedEventSerializer, IMediator mediator)
        {
            _eventStore = eventStore;
            _storedEventSerializer = storedEventSerializer;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(UpgradeDonorState notification, CancellationToken cancellationToken)
        {
            var eventsModels = await _eventStore.GetDonorEvents(notification.DonorId);
            var eventPossitionToAdd = eventsModels.Count + 1;
            var events = eventsModels.Select(e => _storedEventSerializer.Deserialize(e.EventJson, e.EventType)).ToArray();

            var donor = new Donor(events);
            donor.UpgradeDonorState();

            var storedEvents = donor.Changes.Select(c => new StoredEvent {
                EventStreamId = $"Donor-{notification.DonorId}",
                EventPosition = eventPossitionToAdd++,
                EventType = c.GetType().Name,
                EventJson = _storedEventSerializer.Serialize(c)
            }).ToList();

            await _eventStore.UploadEvents(storedEvents);

            foreach (var @event in donor.Changes)
            {
                await _mediator.Publish(@event);
            }

            return donor.Id;
        }
    }
}
