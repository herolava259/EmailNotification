using Payment.Domain.PaymentContext.Enumerations;

namespace Payment.Domain.PaymentContext;

public sealed partial class PaymentAggregate
{
    public PaymentMethodType MethodType { get; private set; } = PaymentMethodType.Bank;

    public decimal Amount { get; set; } 


}
