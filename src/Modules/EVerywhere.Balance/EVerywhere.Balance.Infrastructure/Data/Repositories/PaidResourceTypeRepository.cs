using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;

namespace EVerywhere.Balance.Infrastructure.Data.Repositories;

public class PaidResourceTypeRepository(IBalanceDbContext context) : BaseRepository<PaidResourceType>(context), IPaidResourceTypeRepository;