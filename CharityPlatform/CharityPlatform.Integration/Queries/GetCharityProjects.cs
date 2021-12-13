using CharityPlatform.DAL.Models;
using CharityPlatform.DAL.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Queries
{
    public class GetCharityProjects : IRequest<List<CharityProjectEntity>>
    {
    }

    public class GetCharityProjectsHandler : IRequestHandler<GetCharityProjects, List<CharityProjectEntity>>
    {
        private readonly ReadModelReader _readModelReader;

        public GetCharityProjectsHandler(ReadModelReader readModelReader)
        {
            _readModelReader = readModelReader;
        }

        public async Task<List<CharityProjectEntity>> Handle(GetCharityProjects notification, CancellationToken cancellationToken)
        {
            return await _readModelReader.GetCharityProjects();
        }
    }
}
