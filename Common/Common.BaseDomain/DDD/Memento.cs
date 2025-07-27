using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Generic.DDD;

public interface IMementor
{
}

public interface IOriginator
{ }
public interface IOriginator<out TMementor>: IOriginator
    where TMementor : IMementor
{
    TMementor ToSnapshot();
}

public interface IMemento<out TOriginator>: IMementor
    where TOriginator : IOriginator
{
    public TOriginator ToOriginator();

}
