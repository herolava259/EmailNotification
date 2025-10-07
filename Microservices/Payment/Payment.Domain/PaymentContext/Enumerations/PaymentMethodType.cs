namespace Payment.Domain.PaymentContext.Enumerations;

public enum PaymentMethodType: ushort
{
    Cash = 0,
    BankTranfer = 1,
    DebitCard = 2,
    EWallet = 3,
    Cryptocurrency = 4,
    Autopay = 5,
    Bank = 6,
    CrediCard = 7,
    Voucher = 8,
    PrepaidCard = 9,
    Checks = 10,
    OtherKind = 16,
}
