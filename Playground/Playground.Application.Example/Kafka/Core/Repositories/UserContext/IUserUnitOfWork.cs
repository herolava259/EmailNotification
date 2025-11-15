using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Core.Repositories.UserContext;

public interface IUserUnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }
}
