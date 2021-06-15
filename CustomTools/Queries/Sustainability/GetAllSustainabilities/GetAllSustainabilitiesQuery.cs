using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CustomTools.Data.Access.DAL.DTOs.Sustainability;
using CustomTools.Data.Access.DAL.Interfaces.Sustainability;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomTools.Api.Queries.Sustainability.GetAllSustainabilities
{
    public class GetAllSustainabilitiesQuery : IRequest<IEnumerable<SustainabilityDto>>
    {
        public class GetAllSustainabilitiesHandler : IRequestHandler<GetAllSustainabilitiesQuery, IEnumerable<SustainabilityDto>>
        {
            private readonly ISustainabilityRepository _sustainabilityRepository;
            private readonly ILogger<GetAllSustainabilitiesQuery.GetAllSustainabilitiesHandler> _logger;
            private readonly IMapper _mapper;

            public GetAllSustainabilitiesHandler(ISustainabilityRepository sustainabilityRepository, ILogger<GetAllSustainabilitiesQuery.GetAllSustainabilitiesHandler> logger, IMapper mapper)
            {
                _sustainabilityRepository = sustainabilityRepository;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<IEnumerable<SustainabilityDto>> Handle(GetAllSustainabilitiesQuery request,
                CancellationToken cancellationToken)
            {
                var sustainabilites = await _sustainabilityRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<SustainabilityDto>>(sustainabilites);
            }
        }
    }
}