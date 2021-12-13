using CharityPlatform.DAL.Models;
using CharityPlatform.DAL.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Queries
{
    public class GetOrganizations : IRequest<List<CharityOrganizationEntity>>
    {
    }

    public class GetOrganizationsHandler : IRequestHandler<GetOrganizations, List<CharityOrganizationEntity>>
    {
        private readonly ReadModelReader _readModelReader;

        public GetOrganizationsHandler(ReadModelReader readModelReader)
        {
            _readModelReader = readModelReader;
        }

        public async Task<List<CharityOrganizationEntity>> Handle(GetOrganizations notification, CancellationToken cancellationToken)
        {
            return await _readModelReader.GetCharityOrganizations();
        }
    }
}
