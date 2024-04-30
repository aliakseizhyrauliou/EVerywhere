using System.Data;
using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.Balance.Domain.Enums;
using EVerywhere.Balance.Domain.Events.PaymentSystemWidgetGenerations;
using EVerywhere.Balance.Domain.Services;
using EVerywhere.ModulesCommon.Application.Exceptions;
using EVerywhere.ModulesCommon.Application.Interfaces;
using MediatR;

namespace EVerywhere.Balance.Application.Features.PaymentSystemWidgetFeatures.Commands;

/// <summary>
/// Создание виджета
/// </summary>
public record CreateWidgetCommand : IRequest
{
    public required WidgetReason WidgetReason { get; set; }
    public required string OperatorId { get; set; }
    public string? PaidResourceId { get; set; }
    public string? AdditionalData { get; set; }
    public required long PaidResourceTypeId { get; set; }
    public decimal Amount { get; set; }
    public required long PaymentSystemConfigurationId { get; set; }
}

public sealed class CreateWidgetCommandHandler(
    IPaymentSystemService paymentSystemService,
    IPaymentSystemConfigurationRepository paymentSystemConfigurationRepository,
    IPaymentSystemWidgetGenerationRepository paymentSystemWidgetGenerationRepository,
    IUser currentUser)
    : IRequestHandler<CreateWidgetCommand>
{
    private const string DefaultPaidResourceIdNameInCaseCreatePaymentMethod = "CREATE PAYMENT METHOD";
    private const string DefaultPaidResourceIdNameInCasePayment = "PAYMENT";
    
    public async Task Handle(CreateWidgetCommand request,
        CancellationToken cancellationToken)
    {
        var currentPaymentSchemaConfiguration =
            await paymentSystemConfigurationRepository.GetByIdAsync(request.PaymentSystemConfigurationId, cancellationToken);

        if (currentPaymentSchemaConfiguration is null)
        {
            throw new NotFoundException("payment_system_configuration_not_found");
        }
        
        //Создам модель, которая отражет причину открытия виджета платежной системы

        var paymentSystemWidgetGeneration = BuildPaymentSystemWidgetGeneration(currentPaymentSchemaConfiguration.Id, request);


        paymentSystemWidgetGeneration.AddDomainEvent(new PaymentSystemWidgetGenerationCreatedEvent(paymentSystemWidgetGeneration));

        await using var transaction = await paymentSystemWidgetGenerationRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);

        try
        {
            await paymentSystemWidgetGenerationRepository.DisableAllUserWidgetsAsync(paymentSystemWidgetGeneration.UserId, cancellationToken);
            await paymentSystemWidgetGenerationRepository.InsertAsync(paymentSystemWidgetGeneration, cancellationToken);

            var url = await paymentSystemService.GeneratePaymentSystemWidget(paymentSystemWidgetGeneration,
                currentPaymentSchemaConfiguration,
                cancellationToken);

            paymentSystemWidgetGeneration.Url = url;
            paymentSystemWidgetGeneration.IsSuccess = true;

            await paymentSystemWidgetGenerationRepository.UpdateAsync(paymentSystemWidgetGeneration, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception) 
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private PaymentSystemWidget BuildPaymentSystemWidgetGeneration(long currentPaymentSystemConfiguration,
        CreateWidgetCommand request)  
    {
        return new PaymentSystemWidget
        {
            UserId = currentUser.Id!,
            FirstName = currentUser.FirstName!,
            LastName = currentUser.LastName!,
            WidgetReason = request.WidgetReason,
            PaymentSystemConfigurationId = currentPaymentSystemConfiguration,
            PaidResourceId = request.PaidResourceId ?? DefaultPaidResourceIdNameInCaseCreatePaymentMethod,
            OperatorId = request.OperatorId,
            AdditionalData = request.AdditionalData,
            PaidResourceTypeId = request.PaidResourceTypeId,
            Amount = request.Amount
        };
    }
}