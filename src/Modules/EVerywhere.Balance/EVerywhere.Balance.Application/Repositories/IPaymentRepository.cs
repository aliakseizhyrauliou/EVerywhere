using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Interfaces;

namespace EVerywhere.Balance.Application.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>;