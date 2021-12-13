using CharityPlatform.Domain.OrganizationConfigurationContext.Enums;
using CharityPlatform.Domain.OrganizationConfigurationContext.Errors;
using CharityPlatform.Domain.OrganizationConfigurationContext.Events;
using CharityPlatform.SharedKernel;
using System;
using System.Collections.Generic;

namespace CharityPlatform.Domain.OrganizationConfigurationContext.Entities
{
    public class Organization : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public OrganizationType OrganizationType { get; private set; }
        public DateTime CreationDate { get; set; }
        public List<Guid> Masters { get; set; }

        public Organization(Event[] events)
        {
            foreach (var @event in events)
            {
                ChangeState(@event);
            }
        }

        public Organization(string name,
            string description,
            Guid master,
            OrganizationType organizationType)
        {

            var organizationCreated = new OrganizationCreated
            {
                OrganizationId = Guid.NewGuid(),
                Name = name,
                Description = description,
                Masters = new List<Guid> { master },
                OrganizationType = organizationType,
                CreationDate = DateTime.Now
            };

            Apply(organizationCreated);
        }

        public void AddMaster(Guid master)
        {
            if (master == Guid.Empty)
            {
                throw new InvalidMasterIdError();
            }

            var masterAdded = new MasterAdded
            {
                OrganizationId = Id,
                MasterId = master
            };

            Apply(masterAdded);
        }

        public void RemoveMaster(Guid master)
        {
            if (master == Guid.Empty)
            {
                throw new InvalidMasterIdError();
            }

            var masterRemoved = new MasterRemoved
            {
                OrganizationId = Id,
                MasterId = master
            };

            Apply(masterRemoved);
        }

        public void UpdateOrganization(string name, string description, OrganizationType organizationType)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOrganizationNameError();
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidOrganizationDescriptionError();
            }

            var masterRemoved = new OrganizationUpdated
            {
                Name = name,
                Description =description,
                OrganizationId = Id,
                OrganizationType = organizationType
            };

            Apply(masterRemoved);
        }

        protected override void ChangeState(Event @event)
        {
            When((dynamic)@event);
        }

        private void When(OrganizationCreated @event)
        {
            Name = @event.Name;
            Id = @event.OrganizationId;
            OrganizationType = @event.OrganizationType;
            Masters = @event.Masters;
            CreationDate = @event.CreationDate;
            Description = @event.Description;
        }

        private void When(MasterAdded @event)
        {
            Masters.Add(@event.MasterId);
        }

        private void When(MasterRemoved @event)
        {
            Masters.Remove(@event.MasterId);
        }

        private void When(OrganizationUpdated @event)
        {
            Name = @event.Name;
            Description = @event.Description;
            OrganizationType = @event.OrganizationType;
        }
    }
}
