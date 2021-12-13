using CharityPlatform.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharityPlatform.DAL.Interfaces
{
    public interface IEventStoreRepository
    {
        Task<List<StoredEvent>> GetDonorEvents(Guid donorId);

        Task<List<StoredEvent>> GetCharityRequest(Guid donationRequestId);

        Task<List<StoredEvent>> GetOrganization(Guid organizationId);

        Task UploadEvents(List<StoredEvent> events);
    }
}
