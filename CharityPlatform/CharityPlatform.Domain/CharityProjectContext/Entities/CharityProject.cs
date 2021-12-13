using CharityPlatform.Domain.CharityProjectContext.Errors;
using CharityPlatform.Domain.CharityProjectContext.Events;
using CharityPlatform.SharedKernel;
using System;

namespace CharityPlatform.Domain.CharityProjectContext.Entities
{
    public class CharityProject : Entity<Guid>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid CharityOrganizationId { get; private set; }
        public int DonationGoal { get; set; }
        public int AlreadyDonated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }

        public CharityProject(Event[] events)
        {
            foreach (var @event in events)
            {
                ChangeState(@event);
            }
        }

        public CharityProject(string title,
            string description,
            int donationGoal,
            Guid charityOrganizationId,
            DateTime startDate,
            DateTime endDate)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new InvalidRequestTitleFormatError();
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidRequestDescriptionFormatError();
            }

            if (donationGoal < 0)
            {
                throw new InvalidDonationGoalError(donationGoal);
            }

            if (startDate > endDate)
            {
                throw new InvalidCharityRequestStartEndDateError();
            }

            var charityRequestCreated = new CharityRequestCreated
            {
                Title = title,
                AlreadyDonated = 0,
                CharityOrganizationId = charityOrganizationId,
                Description = description,
                DonationGoal = donationGoal,
                CharityRequestId = Guid.NewGuid(),
                StartDate = startDate,
                EndDate = endDate
            };

            Apply(charityRequestCreated);
        }

        public void Approve()
        {
            var charityRequestApproved = new CharityRequestApproved
            {
                CharityRequestId = Id,
            };

            Apply(charityRequestApproved);
        }

        public void Reject()
        {
            var charityRequestRejected = new CharityRequestRejected
            {
                CharityRequestId = Id,
            };

            Apply(charityRequestRejected);
        }

        public void Donate(int amount, Guid donorId)
        {
            if (DateTime.Now > EndDate || DateTime.Now < StartDate)
            {
                throw new DonateOnUnavailableCharityRequestError();
            }

            if (amount < 0 || amount + AlreadyDonated > DonationGoal)
            {
                throw new InvalidDonateAmountError(amount);
            }

            var donateAdded = new DonateAdded
            {
                DonateAmount = amount,
                CharityOrganizationId = CharityOrganizationId,
                DonorId = donorId,
                CharityRequestId = Id,
            };

            Apply(donateAdded);
        }

        public void When(CharityRequestApproved @event)
        {
            IsApproved = true;
            IsRejected = false;
        }

        public void When(CharityRequestRejected @event)
        {
            IsApproved = false;
            IsRejected = true;
        }


        public void CloseCharityRequest()
        {
            var charityRequestClosed = new CharityRequestClosed
            {
                CharityRequestId = Id,
                EndDate = EndDate.AddDays(-1)
            };

            Apply(charityRequestClosed);
        }

        protected override void ChangeState(Event @event)
        {
            When((dynamic)@event);
        }

        private void When(CharityRequestCreated @event)
        {
            Id = @event.CharityRequestId;
            DonationGoal = @event.DonationGoal;
            AlreadyDonated = @event.AlreadyDonated;
            Title = @event.Title;
            Description = @event.Description;
            CharityOrganizationId = @event.CharityOrganizationId;
            StartDate = @event.StartDate;
            EndDate = @event.EndDate;
        }

        private void When(DonateAdded @event)
        {
            AlreadyDonated += @event.DonateAmount;
        }

        private void When(CharityRequestClosed @event)
        {
            EndDate = @event.EndDate;
        }
    }
}
