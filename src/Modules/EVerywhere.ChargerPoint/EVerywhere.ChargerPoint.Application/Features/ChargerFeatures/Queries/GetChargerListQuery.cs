using AutoMapper;
using EVerywhere.ChargerPoint.Application.Repositories;
using EVerywhere.ChargerPoint.Domain.Entities;
using EVerywhere.ChargerPoint.Domain.Enums;
using MediatR;

namespace EVerywhere.ChargerPoint.Application.Features.ChargerFeatures.Queries;

public class GetChargerListQuery : IRequest<List<ChargerDto>>
{
    
}

public class GetChargerListQueryHandler(IChargerRepository chargerRepository, IMapper mapper) : IRequestHandler<GetChargerListQuery, List<ChargerDto>>
{
    public async Task<List<ChargerDto>> Handle(GetChargerListQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<List<ChargerDto>>(await chargerRepository.GetListAsync(cancellationToken));
    }
}

public class ChargerDto
{
    public long Id { get; set; }
    public long OperatorId { get; set; }
    public long AggregatorId { get; set; }
    public string? SerialNumber { get; set; }
    public string? WebId { get; set; }
    public string? Address { get; set; }
    public string? OperatorSystemChargerId { get; set; }
    public ChargerStatus Status { get; set; }
    public bool IsConnected { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
    public ICollection<ConnectorDto>? Connectors { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Charger, ChargerDto>();
        }
    }
}

public class ConnectorDto
{
    public long Id { get; set; }
    public int Number { get; set; }
    public long ChargerId { get; set; }
    public Charger? Charger { get; set; }
    public double Power { get; set; }
    public string? SerialNum { get; set; }
    public bool Tariffed { get; set; }
    public ConnectorStatus Status { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Connector, ConnectorDto>();
        }
    }
}