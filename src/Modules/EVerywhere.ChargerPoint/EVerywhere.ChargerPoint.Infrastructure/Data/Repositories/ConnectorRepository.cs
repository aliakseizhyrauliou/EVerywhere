using EVerywhere.ChargerPoint.Application.Interfaces;
using EVerywhere.ChargerPoint.Application.Repositories;
using EVerywhere.ChargerPoint.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Implementations;

namespace EVerywhere.ChargerPoint.Infrastructure.Data.Repositories;

public class ConnectorRepository(IChargerPointDbContext context) : BaseRepository<Connector, IChargerPointDbContext>(context), IConnectorRepository;