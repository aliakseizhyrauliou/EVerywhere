using EVerywhere.Balance.UseCases.Holds.Dtos;
using EVerywhere.ModulesCommon.UseCase;

namespace EVerywhere.Balance.UseCases.Holds.Interfaces;

public interface IHoldUseCases : IBaseUseCases
{
    Task<CreatedEntityDto<long>> HoldWithSelectedPaymentMethod(HoldWithSelectedPaymentMethodDto dto, CancellationToken cancellationToken = default);
}