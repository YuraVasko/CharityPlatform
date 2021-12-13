using CharityPlatform.DAL.Models;
using CharityPlatform.DAL.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Queries
{
    public class GetDonationsByProjectId : IRequest<List<DonationRecordEntity>>
    {
        public Guid CharityProjectId { get; set; }
    }

    public class GetDonationsByProjectIdHandler : IRequestHandler<GetDonationsByProjectId, List<DonationRecordEntity>>
    {
        private readonly ReadModelReader _readModelReader;

        public GetDonationsByProjectIdHandler(ReadModelReader readModelReader)
        {
            _readModelReader = readModelReader;
        }

        public async Task<List<DonationRecordEntity>> Handle(GetDonationsByProjectId notification, CancellationToken cancellationToken)
        {
            return await _readModelReader.GetDonationsByProjectsId(notification.CharityProjectId);
        }
    }
}
