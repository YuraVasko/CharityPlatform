using CharityPlatform.DAL.Models;
using CharityPlatform.DAL.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Queries
{
    public class GetDonorDonations : IRequest<List<DonationRecordEntity>>
    {
        public Guid DonorId { get; set; }
    }

    public class GetDonorDonationsHandler : IRequestHandler<GetDonorDonations, List<DonationRecordEntity>>
    {
        private readonly ReadModelReader _readModelReader;

        public GetDonorDonationsHandler(ReadModelReader readModelReader)
        {
            _readModelReader = readModelReader;
        }

        public async Task<List<DonationRecordEntity>> Handle(GetDonorDonations notification, CancellationToken cancellationToken)
        {
            return await _readModelReader.GetDonorDonations(notification.DonorId);
        }
    }
}
