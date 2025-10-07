using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Application.Example.SagasStateMachine.Dtos;

public class CreateOrderEventDto
{
    public Guid OrderId { get; set; }
}
