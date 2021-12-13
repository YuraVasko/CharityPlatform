using CharityPlatform.Domain.CharityDonorsContext.Enums;
using CharityPlatform.Domain.CharityDonorsContext.Errors;
using CharityPlatform.Domain.CharityDonorsContext.Events;
using CharityPlatform.Domain.CharityDonorsContext.ValueObjects;
using CharityPlatform.SharedKernel;
using System;
using System.Collections.Generic;

namespace CharityPlatform.Domain.CharityDonorsContext.Entities
{
    public class Donor : Entity<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DonorLevel DonorLevel { get; private set; }
        public List<DonationRecord> DonationHistory { get; private set; }

        public Donor(Event[] events)
        {
            foreach(var @event in events)
            {
                ChangeState(@event);
            }
        }

        public Donor(string firsName, string lastName, string email, Guid userId)
        {
            if (string.IsNullOrEmpty(firsName))
            {
                throw new InvalidFirsNameFormatError();
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new InvalidLastNameFormatError();
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidEmailFormatError();
            }

            var donorCreated = new DonorCreated
            {
                DonorId = userId,
                DonationHistory = new List<DonationRecord>(),
                DonorLevel = DonorLevel.Begginer,
                Email = email,
                FirstName = firsName,
                LastName = lastName,
                UserId = userId
            };
            Apply(donorCreated);
        }

        public void UpgradeDonorState()
        {
            DonorLevel donorLevelToUpdate = DonorLevel;

            switch (DonorLevel)
            {
                case DonorLevel.Begginer: donorLevelToUpdate = DonorLevel.Amateur; break;
                case DonorLevel.Amateur: donorLevelToUpdate = DonorLevel.Profesional; break;
                case DonorLevel.Profesional: donorLevelToUpdate = DonorLevel.WorldClass; break;
                default: throw new InvalidDonorStateError(Id);
            }

            var donorCreated = new DonorLevelUpdated
            {
                DonorId = Id,
                DonorLevelToUpdate = donorLevelToUpdate
            };

            Apply(donorCreated);
        }

        public void AddDonationRecord(int donationAmount, Guid charityProjectId)
        {
            if (donationAmount < 0)
            {
                throw new InvalidDonationAmountError(donationAmount, Id);
            }

            var donationHistoryAdded = new DonationHistoryAdded
            {
                DonationAmount = donationAmount,
                CharityProjectId = charityProjectId,
                DonorId = Id
            };

            Apply(donationHistoryAdded);
        }

        protected override void ChangeState(Event @event)
        {
            When((dynamic)@event);
        }

        private void When(DonorCreated @event)
        {
            Id = @event.DonorId;
            DonationHistory = @event.DonationHistory;
            DonorLevel = @event.DonorLevel;
            Email = @event.Email;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }

        private void When(DonationHistoryAdded @event)
        {
            DonationHistory.Add(new DonationRecord(@event.DonationAmount, @event.CharityProjectId));
        }

        private void When(DonorLevelUpdated @event)
        {
            DonorLevel = @event.DonorLevelToUpdate;
        }
    }
}
