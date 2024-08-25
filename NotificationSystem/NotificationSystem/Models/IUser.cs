using NotificationSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Models
{
    internal interface IUser
    {
        Guid GetUserId();

    }
}
