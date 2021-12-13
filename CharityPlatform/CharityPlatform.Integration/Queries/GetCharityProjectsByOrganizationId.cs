using CharityPlatform.DAL.Models;
using CharityPlatform.DAL.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Queries
{
    public class GetCharityProjectsByOrganizationId : IRequest<List<CharityProjectEntity>>
    {
        public Guid OrganizationId { get; set; }
    }

    public class GetCharityProjectByOrganizationIdHandler : IRequestHandler<GetCharityProjectsByOrganizationId, List<CharityProjectEntity>>
    {
        private readonly ReadModelReader _readModelReader;

        public GetCharityProjectByOrganizationIdHandler(ReadModelReader readModelReader)
        {
            _readModelReader = readModelReader;
        }

        public async Task<List<CharityProjectEntity>> Handle(GetCharityProjectsByOrganizationId notification, CancellationToken cancellationToken)
        {
            return await _readModelReader.GetCharityProjectsByOrganizationId(notification.OrganizationId);
        }
    }
}
