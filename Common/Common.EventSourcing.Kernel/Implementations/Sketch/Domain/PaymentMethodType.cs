using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.EventSourcing.Kernel.Implementations.Sketch.Domain;

public enum PaymentMethodType: ushort
{
    Credit = 0, 
    Cash = 1,
    EWallet = 2,
    Voucher = 3,
}
