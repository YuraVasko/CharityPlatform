using CharityPlatform.DAL.Interfaces;
using CharityPlatform.DAL.Models;
using CharityPlatform.Domain.CharityProjectContext.Entities;
using CharityPlatform.Integration.Infrastructure;
using CharityPlatform.LinqPay.Integration;
using CharityPlatform.LinqPay.Integration.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Commands
{
    public class AddDonation : IRequest<AddDonationResponse>
    {
        public int Amount { get; set; }
        public Guid DonorId { get; set; }
        public Guid CharityRequestId { get; set; }

        public AddDonation(int amount, Guid donorId, Guid charityRequestId)
        {
            Amount = amount;
            DonorId = donorId;
            CharityRequestId = charityRequestId;
        }
    }

    public class AddDonationResponse 
    {
        public PaymentFormDetails PaymentForm { get; set; }
        public Guid DonationId { get; set; }
    }

    public class AddDonationHandler : IRequestHandler<AddDonation, AddDonationResponse>
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly StoredEventSerializer _storedEventSerializer;
        private readonly IMediator _mediator;
        private readonly ILiqPayClient _liqPayClient;

        public AddDonationHandler(IEventStoreRepository eventStore, StoredEventSerializer storedEventSerializer, IMediator mediator, ILiqPayClient liqPayClient)
        {
            _eventStore = eventStore;
            _storedEventSerializer = storedEventSerializer;
            _mediator = mediator;
            _liqPayClient = liqPayClient;
        }

        public async Task<AddDonationResponse> Handle(AddDonation notification, CancellationToken cancellationToken)
        {
            var eventsModels = await _eventStore.GetCharityRequest(notification.CharityRequestId);
            var eventPossitionToAdd = eventsModels.Count + 1;
            var events = eventsModels.Select(e => _storedEventSerializer.Deserialize(e.EventJson, e.EventType)).ToArray();

            var charityRequest = new CharityProject(events);
            charityRequest.Donate(notification.Amount, notification.DonorId);

            var storedEvents = charityRequest.Changes.Select(c => new StoredEvent
            {
                EventStreamId = $"CharityRequest-{notification.CharityRequestId}",
                EventPosition = eventPossitionToAdd++,
                EventType = c.GetType().Name,
                EventJson = _storedEventSerializer.Serialize(c)
            }).ToList();

            await _eventStore.UploadEvents(storedEvents);
            var paymentFormData = await _liqPayClient.GetPaymentForm(
                new GetPaymentFormRequestModel
                {
                    Action = "pay",
                    Amount = notification.Amount,
                    Currency = "UAH",
                    Description = "Description",
                    OrderId = notification.CharityRequestId.ToString() + "-" + notification.DonorId.ToString() + Guid.NewGuid()
                });

            foreach (var @event in charityRequest.Changes)
            {
                await _mediator.Publish(@event);
            }

            return new AddDonationResponse
            {
                DonationId = charityRequest.Id,
                PaymentForm = paymentFormData
            };
        }
    }
}
