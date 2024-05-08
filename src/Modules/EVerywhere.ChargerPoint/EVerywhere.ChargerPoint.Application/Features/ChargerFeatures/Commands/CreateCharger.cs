using EVerywhere.ChargerPoint.Application.Interfaces;
using EVerywhere.ChargerPoint.Application.Repositories;
using EVerywhere.ChargerPoint.Domain.Entities;
using EVerywhere.ModulesCommon.Application.Interfaces;
using EVerywhere.ModulesCommon.UseCase;
using MediatR;

namespace EVerywhere.ChargerPoint.Application.Features.ChargerFeatures.Commands;

public class CreateChargerCommand : IRequest<CreatedEntityDto<long>>
{
    public string? Address { get; set; }
    public string? OperatorSystemChargerId { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
}


public class CreateChargerCommandHandler(IChargerRepository chargerRepository, 
    IUser user) : IRequestHandler<CreateChargerCommand, CreatedEntityDto<long>>
{
    public async Task<CreatedEntityDto<long>> Handle(CreateChargerCommand request,
        CancellationToken cancellationToken)
    {
        var charger = new Charger
        {
            OperatorId = (long)user.OperatorId!,
            AggregatorId = (long)user.AggregatorId!,
            Address = request.Address,
            OperatorSystemChargerId = request.OperatorSystemChargerId,
            Lat = request.Lat,
            Lon = request.Lon
        };

        await chargerRepository.InsertAsync(charger, cancellationToken);

        return new CreatedEntityDto<long>(charger.Id);
    }
}
