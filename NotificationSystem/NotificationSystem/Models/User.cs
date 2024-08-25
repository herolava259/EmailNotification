using NotificationSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Models
{
    internal class User: IUser
    {

        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public List<Guid> PublisherIds { get; set; } = new();

        public Guid GetUserId()
        {
            return Id;
        }

        
    }
}
