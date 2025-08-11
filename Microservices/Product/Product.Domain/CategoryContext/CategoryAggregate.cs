using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.CategoryContext;

public sealed partial class CategoryAggregate
{
    public string Name { get; private set; } = String.Empty;


}
