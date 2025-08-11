using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductComplexContext.Enumerations;

public enum EProductPropertyBetweenType: ulong
{
    Include = 100_000_000,
    Exclude = 100_000_001
}
