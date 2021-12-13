using CharityPlatform.DAL.Models;
using CharityPlatform.DAL.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Queries
{
    public class GetDonor : IRequest<DonorEntity>
    {
        public Guid DonorId { get; set; }
    }

    public class GetDonorHandler : IRequestHandler<GetDonor, DonorEntity>
    {
        private readonly ReadModelReader _readModelReader;

        public GetDonorHandler(ReadModelReader readModelReader)
        {
            _readModelReader = readModelReader;
        }

        public async Task<DonorEntity> Handle(GetDonor notification, CancellationToken cancellationToken)
        {
            return await _readModelReader.GetDonor(notification.DonorId);
        }
    }
}
