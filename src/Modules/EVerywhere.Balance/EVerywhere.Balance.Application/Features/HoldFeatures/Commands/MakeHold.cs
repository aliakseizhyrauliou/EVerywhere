using System.Data;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.Balance.Domain.Events.Holds;
using EVerywhere.Balance.Domain.Exceptions;
using EVerywhere.Balance.Domain.Services;
using EVerywhere.ModulesCommon.Application.Exceptions;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace EVerywhere.Balance.Application.Features.HoldFeatures.Commands;

public record MakeHoldCommand : IRequest<long>
{
    public required string UserId { get; set; }
    public required string PaidResourceId { get; set; }
    public required string OperatorId { get; set; }
    public required decimal Amount { get; set; }
    public required int PaidResourceTypeId { get; set; }
    public required int PaymentMethodId { get; set; }
    public required int PaymentSystemConfigurationId { get; set; }
    public Dictionary<string, string>? AdditionalData { get; set; }
}

public class MakeHoldCommandValidator : AbstractValidator<MakeHoldCommand>
{
    public MakeHoldCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);
    }
}

/// <summary>
/// Холдирует сумму и записывает результат в базу данных
/// </summary>
/// <param name="paymentSystemService"></param>
/// <param name="repository"></param>
/// <param name="mapper"></param>
public sealed class MakeHoldCommandHandler(IPaymentSystemService paymentSystemService, 
    IPaymentMethodRepository paymentMethodRepository,
    IHoldRepository repository,
    IPaymentSystemConfigurationRepository configurationRepository,
    IPaidResourceTypeRepository paidResourceTypeRepository) : IRequestHandler<MakeHoldCommand, long>
{
    public async Task<long> Handle(MakeHoldCommand request, 
        CancellationToken cancellationToken)
    {
        var isPaidResourceTypeExist = await IsPaidResourceTypeExist(request.PaidResourceTypeId, cancellationToken);

        if (!isPaidResourceTypeExist)
        {
            throw new NotFoundException("paid_resource_type_not_found");
        }
        
        //Получаем платежный метод
        var paymentMethod = await paymentMethodRepository.GetByIdAsync(request.PaymentMethodId, cancellationToken);

        if (paymentMethod is null)
        {
            throw new NotFoundException("payment_method_was_not_found");
        }

        var paymentSystemConfiguration =
            await configurationRepository.GetByIdAsync(request.PaymentSystemConfigurationId, cancellationToken);

        if (paymentSystemConfiguration is null)
        {
            throw new Exception("payment_system_configuration_not_found");
        }
        //Создаем модель 
        var domainModel = new Hold
        {
            UserId = request.UserId,
            PaymentMethodId = request.PaymentMethodId,
            PaidResourceId = request.PaidResourceId,
            OperatorId = request.OperatorId,
            Amount = request.Amount,
            PaidResourceTypeId = request.PaidResourceTypeId,
            AdditionalData = JsonConvert.SerializeObject(request.AdditionalData),
            PaymentSystemConfigurationId = paymentSystemConfiguration.Id
        };
        
        //Данный метод ничего не сохранаяет в базу, это не его ответственность
        //Domain слой вообще не должен быть в курсе, что что-то где-то храниться
        var makeHoldRequestToPaymentSystemResult = await paymentSystemService.Hold(domainModel,
            paymentMethod, 
            paymentSystemConfiguration, 
            cancellationToken);

        if (makeHoldRequestToPaymentSystemResult is { IsOk: true, Hold: not null })
        {
            var hold = makeHoldRequestToPaymentSystemResult.Hold;
            
            await using var transaction = await repository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);

            try
            {
                hold.AddDomainEvent(new MakeHoldEvent(hold));
                await repository.InsertAsync(hold, cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
        else
        {
            throw new PaymentSystemException(makeHoldRequestToPaymentSystemResult.FriendlyErrorMessage);
        }
        
        return makeHoldRequestToPaymentSystemResult.Hold.Id;
    }
    
    private async Task<bool> IsPaidResourceTypeExist(int paidResourceTypeId, 
        CancellationToken cancellationToken)
    {
        return await paidResourceTypeRepository.AnyAsync(x => x.Id == paidResourceTypeId, cancellationToken);
    }
}

