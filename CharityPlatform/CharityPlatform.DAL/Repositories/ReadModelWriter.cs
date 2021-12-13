using CharityPlatform.DAL.Models;
using CharityPlatform.Domain.CharityDonorsContext.Entities;
using CharityPlatform.Domain.CharityDonorsContext.Events;
using CharityPlatform.Domain.CharityProjectContext.Events;
using CharityPlatform.Domain.OrganizationConfigurationContext.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.DAL.Repositories
{
    public class ReadModelWriter 
        : INotificationHandler<DonorCreated>,
          INotificationHandler<DonorLevelUpdated>,
          INotificationHandler<DonationHistoryAdded>,
          INotificationHandler<OrganizationCreated>,
          INotificationHandler<OrganizationUpdated>,
          INotificationHandler<MasterAdded>,
          INotificationHandler<MasterRemoved>,
          INotificationHandler<DonateAdded>,
          INotificationHandler<CharityRequestCreated>,
          INotificationHandler<CharityRequestClosed>,
          INotificationHandler<CharityRequestApproved>,
          INotificationHandler<CharityRequestRejected>
    {
        private readonly CharityPlatformContext _context;

        public ReadModelWriter(CharityPlatformContext context)
        {
            _context = context;
        }

        public async Task Handle(DonorCreated notification, CancellationToken cancellationToken)
        {
            _context.Donors.Add(new DonorEntity 
            {
                Id = notification.DonorId,
                DonationHistory = new System.Collections.Generic.List<DonationRecordEntity>(),
                DonorLevel = notification.DonorLevel,
                UserId = notification.UserId,
            });

            await _context.SaveChangesAsync();
        }

        public async Task Handle(DonorLevelUpdated notification, CancellationToken cancellationToken)
        {
            var donor = await _context.Donors.FirstOrDefaultAsync(d=>d.Id == notification.DonorId);
            donor.DonorLevel = notification.DonorLevelToUpdate;

            await _context.SaveChangesAsync();
        }

        public async Task Handle(DonationHistoryAdded notification, CancellationToken cancellationToken)
        {
            var donor = await _context.Donors
                .Include(d=>d.DonationHistory)
                .FirstOrDefaultAsync(d => d.Id == notification.DonorId);
            if (donor.DonationHistory == null)
            {
                donor.DonationHistory = new System.Collections.Generic.List<DonationRecordEntity>();
            }

            donor.DonationHistory.Add(new DonationRecordEntity
            {
                CharityProjectId = notification.CharityProjectId,
                DonationAmount = notification.DonationAmount,
                DonorId = notification.DonorId,
                Id = Guid.NewGuid()
            });

            await _context.SaveChangesAsync();
        }

        public async Task Handle(OrganizationCreated notification, CancellationToken cancellationToken)
        {
            await _context.CharityOrganizations.AddAsync(
                new CharityOrganizationEntity 
                {
                    CreationDate = notification.CreationDate,
                    Description = notification.Description,
                    Id = notification.OrganizationId,
                    Name = notification.Name,
                    OrganizationType = notification.OrganizationType,
                    Masters = await _context.Users.Where(u=> notification.Masters.Contains(u.Id)).ToListAsync()
                });

            await _context.SaveChangesAsync();
        }

        public async Task Handle(OrganizationUpdated notification, CancellationToken cancellationToken)
        {
            var organization = await _context.CharityOrganizations.FirstOrDefaultAsync(c => c.Id == notification.OrganizationId);
            organization.Description = notification.Description;
            organization.Id = notification.OrganizationId;
            organization.Name = notification.Name;
            organization.OrganizationType = notification.OrganizationType;

            await _context.SaveChangesAsync();
        }

        public async Task Handle(MasterAdded notification, CancellationToken cancellationToken)
        {
            var organization = await _context.CharityOrganizations
                .Include(c => c.Masters)
                .FirstOrDefaultAsync(c => c.Id == notification.OrganizationId);

            organization.Masters.Add(await _context.Users.FirstOrDefaultAsync(u => u.Id == notification.MasterId));

            await _context.SaveChangesAsync();
        }

        public async Task Handle(MasterRemoved notification, CancellationToken cancellationToken)
        {
            var organization = await _context.CharityOrganizations
                .Include(c => c.Masters)
                .FirstOrDefaultAsync(c => c.Id == notification.OrganizationId);

            organization.Masters.Remove(await _context.Users.FirstOrDefaultAsync(u => u.Id == notification.MasterId));

            await _context.SaveChangesAsync();
        }

        public async Task Handle(CharityRequestCreated notification, CancellationToken cancellationToken)
        {
            _context.CharityProjects.Add(new CharityProjectEntity 
            {
                AlreadyDonated = notification.AlreadyDonated,
                CharityOrganizationId = notification.CharityOrganizationId,
                Description = notification.Description,
                DonationGoal = notification.DonationGoal,
                EndDate = notification.EndDate,
                Id = notification.CharityRequestId,
                StartDate = notification.StartDate,
                Title = notification.Title,
            });

            await _context.SaveChangesAsync();
        }

        public async Task Handle(CharityRequestClosed notification, CancellationToken cancellationToken)
        {
            var project = _context.CharityProjects.FirstOrDefault(p => notification.CharityRequestId == p.Id);
            project.EndDate = notification.EndDate;

            await _context.SaveChangesAsync();
        }

        public async Task Handle(DonateAdded notification, CancellationToken cancellationToken)
        {
            await _context.DonationRecords.AddAsync(new DonationRecordEntity
            {
                CharityProjectId = notification.CharityRequestId,
                DonationAmount = notification.DonateAmount,
                DonorId = notification.DonorId,
                Id = Guid.NewGuid()
            });

            var project = await _context.CharityProjects.FirstOrDefaultAsync(c => c.Id == notification.CharityRequestId);
            project.AlreadyDonated += notification.DonateAmount;

            await _context.SaveChangesAsync();
        }

        public async Task Handle(CharityRequestApproved notification, CancellationToken cancellationToken)
        {
            var project = _context.CharityProjects.FirstOrDefault(p => notification.CharityRequestId == p.Id);
            project.IsApproved = true;
            project.IsRejected = false;

            await _context.SaveChangesAsync();
        }

        public async Task Handle(CharityRequestRejected notification, CancellationToken cancellationToken)
        {
            var project = _context.CharityProjects.FirstOrDefault(p => notification.CharityRequestId == p.Id);
            project.IsApproved = false;
            project.IsRejected = true;

            await _context.SaveChangesAsync();
        }
    }
}
