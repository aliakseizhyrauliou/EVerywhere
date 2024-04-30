using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction;
using Barion.Balance.Infrastructure.External.BePaid.BePaidModels.Transaction.TransactionStatus;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.Balance.Domain.Services.ServiceResponses;

namespace EVerywhere.Balance.Infrastructure.External.BePaid.Services;

public partial class BePaidService
{
    private async Task<ProcessVoidHoldPaymentSystemResult> ProcessVoidHoldPaymentSystemResponse(
        Hold hold,
        TransactionRoot transaction,
        CancellationToken cancellationToken = default)
    {
        return transaction.Transaction.Status switch
        {
            TransactionStatus.Successful => ProcessSuccessfulVoidHoldStatus(
                transaction, hold),
            TransactionStatus.Failed => ProcessFailedVoidHoldStatus(
                transaction, hold),
            _ => throw new NotImplementedException()
        };
    }

    private ProcessVoidHoldPaymentSystemResult ProcessSuccessfulVoidHoldStatus(TransactionRoot transaction,
        Hold voidedHold)
    {
        voidedHold.IsVoided = true;

        return new ProcessVoidHoldPaymentSystemResult
        {
            IsOk = true,
            PaymentSystemTransactionId = transaction.Transaction.Id,
            Hold = voidedHold
        };
    }

    private ProcessVoidHoldPaymentSystemResult ProcessFailedVoidHoldStatus(TransactionRoot transaction,
        Hold notVoidedHold)
    {
        return new ProcessVoidHoldPaymentSystemResult
        {
            IsOk = false,
            Hold = notVoidedHold,
            ErrorMessage = transaction.Transaction.Message,
            FriendlyErrorMessage = transaction.Transaction.Message
        };
    }
}