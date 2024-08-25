using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Interfaces
{
    internal interface IObserver<TMessage>
        where TMessage: IMessage
    {
        void OnNotify(TMessage message);
    }
}
