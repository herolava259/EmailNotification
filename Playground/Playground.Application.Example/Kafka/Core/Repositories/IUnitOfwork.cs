using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Core.Repositories;

public interface IUnitOfwork: IDisposable
{


    Task<bool> CompleteAsync();
}
