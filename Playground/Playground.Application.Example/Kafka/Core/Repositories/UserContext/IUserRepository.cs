using Playground.Application.Example.Kafka.Core.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.Kafka.Core.Repositories.UserContext;

public interface IUserRepository: IGenericRepository, IGenericRepository<UserAggregateRoot>
{
}
