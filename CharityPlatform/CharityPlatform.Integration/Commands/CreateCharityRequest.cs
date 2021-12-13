using CharityPlatform.DAL.Interfaces;
using CharityPlatform.DAL.Models;
using CharityPlatform.Domain.CharityProjectContext.Entities;
using CharityPlatform.Integration.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Commands
{
    public class CreateCharityRequest : IRequest<Guid>
    {
        public string Title { get; }
        public string Description { get; }
        public Guid CharityOrganizationId { get; }
        public int DonationGoal { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public Guid MasterId { get; }
        

        public CreateCharityRequest(string title, string description, Guid charityOrganizationId, int donationGoal, DateTime startDate, DateTime endDate, Guid masterId)
        {
            Title = title;
            Description = description;
            CharityOrganizationId = charityOrganizationId;
            DonationGoal = donationGoal;
            StartDate = startDate;
            EndDate = endDate;
            MasterId = masterId;
        }
    }

    public class CreateCharityRequestHandler : IRequestHandler<CreateCharityRequest, Guid>
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly StoredEventSerializer _storedEventSerializer;
        private readonly IMediator _mediator;

        public CreateCharityRequestHandler(IEventStoreRepository eventStore, StoredEventSerializer storedEventSerializer, IMediator mediator)
        {
            _eventStore = eventStore;
            _storedEventSerializer = storedEventSerializer;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateCharityRequest notification, CancellationToken cancellationToken)
        {
            var eventPossitionToAdd = 1;
            var charityRequest = new CharityProject(
                notification.Title, 
                notification.Description, 
                notification.DonationGoal, 
                notification.CharityOrganizationId,
                notification.StartDate,
                notification.EndDate);

            var storedEvents = charityRequest.Changes.Select(c => new StoredEvent
            {
                EventStreamId = $"CharityRequest-{charityRequest.Id}",
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