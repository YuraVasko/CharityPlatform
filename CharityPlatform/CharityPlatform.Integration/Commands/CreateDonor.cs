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
    public class CreateDonor : IRequest<Guid>
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public Guid UserId { get; }

        public CreateDonor(string firstName, string lastName, string email, Guid userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserId = userId;
        }
    }

    public class CrateDonorStateHandler : IRequestHandler<CreateDonor, Guid>
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly StoredEventSerializer _storedEventSerializer;
        private readonly IMediator _mediator;

        public CrateDonorStateHandler(IEventStoreRepository eventStore, StoredEventSerializer storedEventSerializer, IMediator mediator)
        {
            _eventStore = eventStore;
            _storedEventSerializer = storedEventSerializer;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateDonor notification, CancellationToken cancellationToken)
        {
            var eventPossitionToAdd = 1;
            var donor = new Donor(notification.FirstName, notification.LastName, notification.Email, notification.UserId);
            
            var storedEvents = donor.Changes.Select(c => new StoredEvent
            {
                EventStreamId = $"Donor-{donor.Id}",
                EventPosition = eventPossitionToAdd++,
                EventType = c.GetType().Name,
                EventJson = _storedEventSerializer.Serialize(c)
            }).ToList();

            await _eventStore.UploadEvents(storedEvents);

            foreach(var @event in donor.Changes)
            {
                await _mediator.Publish(@event);
            }

            return donor.Id;
        }
    }
}
