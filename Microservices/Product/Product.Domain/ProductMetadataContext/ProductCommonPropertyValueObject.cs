using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductMetadataContext;

public sealed record ProductCommonPropertyValueObject(string Title, string Value, string Description)
{
}
