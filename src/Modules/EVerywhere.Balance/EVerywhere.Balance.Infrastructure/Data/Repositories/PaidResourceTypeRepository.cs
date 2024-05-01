using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Implementations;

namespace EVerywhere.Balance.Infrastructure.Data.Repositories;

public class PaidResourceTypeRepository(IBalanceDbContext context) : BaseRepository<PaidResourceType, IBalanceDbContext>(context), IPaidResourceTypeRepository;