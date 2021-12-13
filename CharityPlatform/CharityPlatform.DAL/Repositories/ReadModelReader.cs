using CharityPlatform.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharityPlatform.DAL.Repositories
{
    public class ReadModelReader
    {
        private readonly CharityPlatformContext _context;

        public ReadModelReader(CharityPlatformContext context)
        {
            _context = context;
        }

        public Task<List<CharityProjectEntity>> GetCharityProjects()
        {
            return _context.CharityProjects.ToListAsync();
        }

        public Task<CharityProjectEntity> GetCharityProjectById(Guid projectId)
        {
            return _context.CharityProjects.FirstOrDefaultAsync(c=>c.Id == projectId);
        }

        public Task<List<CharityProjectEntity>> GetCharityProjectsByOrganizationId(Guid organizationId)
        {
            return _context.CharityProjects.Where(c => c.CharityOrganizationId == organizationId).ToListAsync();
        }

        public Task<List<CharityOrganizationEntity>> GetCharityOrganizations()
        {
            return _context.CharityOrganizations.ToListAsync();
        }

        public Task<CharityOrganizationEntity> GetCharityOrganizationById(Guid organizationId)
        {
            return _context.CharityOrganizations.FirstOrDefaultAsync(c => c.Id == organizationId);
        }

        public Task<List<DonationRecordEntity>> GetDonationsByProjectsId(Guid charityProjectId)
        {
            return _context.DonationRecords.Where(c => c.CharityProjectId == charityProjectId).ToListAsync();
        }

        public Task<List<DonationRecordEntity>> GetDonorDonations(Guid donorId)
        {
            return _context.DonationRecords.Where(c => c.DonorId == donorId).ToListAsync();
        }

        public Task<DonorEntity> GetDonor(Guid donorId)
        {
            return _context.Donors.FirstOrDefaultAsync(c => c.Id == donorId);
        }
    }
}
