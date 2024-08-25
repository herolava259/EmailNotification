using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Interfaces
{
    internal interface IMediator<TMessage, TUser>
        where TMessage : IMessage
        where TUser : IUser
    {
        void PushMessage(TMessage message, Guid publisherId);

        public IPublisher RegisterToPublisher(TUser user);

        ISubcriber<TMessage>? Subcribe(TUser user, Guid otherId);
        void Unsubcribe(Guid subcriberId);
    }
}
