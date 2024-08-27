using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Interfaces
{
    internal interface IPublisher: IRegister
    {
        void PublishMessage(string content);

        void PublishBulkMessages(IEnumerable<string> contents);
    }
}
