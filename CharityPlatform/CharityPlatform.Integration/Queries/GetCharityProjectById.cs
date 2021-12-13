using CharityPlatform.DAL.Models;
using CharityPlatform.DAL.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CharityPlatform.Integration.Queries
{
    public class GetCharityProjectById : IRequest<CharityProjectEntity>
    {
        public Guid CharityProjectId { get; set; }
    }

    public class GetCharityProjectByIdHandler : IRequestHandler<GetCharityProjectById, CharityProjectEntity>
    {
        private readonly ReadModelReader _readModelReader;

        public GetCharityProjectByIdHandler(ReadModelReader readModelReader)
        {
            _readModelReader = readModelReader;
        }

        public async Task<CharityProjectEntity> Handle(GetCharityProjectById notification, CancellationToken cancellationToken)
        {
            return await _readModelReader.GetCharityProjectById(notification.CharityProjectId);
        }
    }
}
