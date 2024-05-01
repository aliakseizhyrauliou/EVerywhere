using System.Data;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.Balance.Domain.Events.PaymentMethods;
using EVerywhere.ModulesCommon.Application.Exceptions;
using EVerywhere.ModulesCommon.Application.Interfaces;
using MediatR;

namespace EVerywhere.Balance.Application.Features.PaymentMethodFeatures.Commands;

public record SelectPaymentMethodCommand : IRequest
{
    public required long Id { get; set; }
    public required string UserId { get; set; }
}

public sealed class SelectPaymentMethodCommandHandler(IPaymentMethodRepository repository,
    IUser currentUser) 
    : IRequestHandler<SelectPaymentMethodCommand>
{
    public async Task Handle(SelectPaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (paymentMethod is null)
        {
            throw new NotFoundException("payment_method_not_found");
        }

        if (!IsUserOwnPaymentMethod(paymentMethod, request))
        {
            throw new ForbiddenAccessException("user_doesnt_own_this_payment_method");
        }

        await using var transaction =
            await repository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);

        try
        {
            await repository.UnselectAllPaymentMethodsAsync(request.UserId, cancellationToken);

            await SelectPaymentMethod(paymentMethod, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static bool IsUserOwnPaymentMethod(PaymentMethod paymentMethod, 
        SelectPaymentMethodCommand request)
    {
        return paymentMethod.UserId.Equals(request.UserId);
    }

    private async Task SelectPaymentMethod(PaymentMethod paymentMethod,
        CancellationToken cancellationToken)
    {
        paymentMethod.IsSelected = true;
        
        paymentMethod.AddDomainEvent(new PaymentMethodSelectedEvent(paymentMethod));

        await repository.UpdateAsync(paymentMethod, cancellationToken);
    }
}