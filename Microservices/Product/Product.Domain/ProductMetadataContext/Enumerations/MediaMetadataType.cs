using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.ProductMetadataContext.Enumerations;

public enum MediaMetadataType: ushort
{
    PNG = 0,
    JPG = 1,
    WEBP = 2,
    TXT = 3,
    WAV = 4,
    MP4 = 5,
    None = 6
}
