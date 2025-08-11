using Product.Domain.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductMetadataContext;

public sealed partial class ProductMediaMetadataEntity : MediaMetadataEntity
{
    public override string FileName => throw new NotImplementedException();


}
