using CharityPlatform.DAL.Models;
using CharityPlatform.DAL.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Queries
{
    public class GetOrganizationById : IRequest<CharityOrganizationEntity>
    {
        public Guid OrganizationId { get; set; }
    }

    public class GetOrganizationByIdHandler : IRequestHandler<GetOrganizationById, CharityOrganizationEntity>
    {
        private readonly ReadModelReader _readModelReader;

        public GetOrganizationByIdHandler(ReadModelReader readModelReader)
        {
            _readModelReader = readModelReader;
        }

        public async Task<CharityOrganizationEntity> Handle(GetOrganizationById notification, CancellationToken cancellationToken)
        {
            return await _readModelReader.GetCharityOrganizationById(notification.OrganizationId);
        }
    }
}
