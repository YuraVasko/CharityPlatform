using CharityPlatform.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityPlatform.DAL
{
    public class CharityPlatformContext : DbContext
    {
        public CharityPlatformContext(DbContextOptions<CharityPlatformContext> options) : base(options)
        {

            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoredEvent>().HasKey(e => new { e.EventStreamId, e.EventPosition });
        }

        public DbSet<StoredEvent> StoredEvents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CharityOrganizationEntity> CharityOrganizations { get; set; }
        public DbSet<CharityProjectEntity> CharityProjects { get; set; }
        public DbSet<DonationRecordEntity> DonationRecords { get; set; }
        public DbSet<DonorEntity> Donors { get; set; }
    }
}
