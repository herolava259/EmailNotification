using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Interfaces
{
    internal interface ISubcriber<TMessage>
        where TMessage: IMessage
    {

        public TMessage ReceiveLatestMessage();

        public IEnumerable<TMessage> ReceiveAllMessage();
        public void Unsubcribe();
    }
}
