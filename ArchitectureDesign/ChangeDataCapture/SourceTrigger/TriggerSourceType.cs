using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeDataCapture.SourceTrigger;

public enum TriggerSourceType: ushort
{
    None = 0,
    Client = 1,
    Schedule = 2,
    OtherService = 3,
    Internal = 4,
}

public enum TriggerType: ushort
{
    None = 0,
    RestfulApi = 1,
    Grpc = 2,
    MesageBroker = 3, // kafka, rabbit-mq
    PubSub = 4, // redis
    Webhook = 5,  // https
    Background = 6,
}


