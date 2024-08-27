using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Models
{
    public class Post : IMessage
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string? Content { get; set; }

        public DateTime Timestamp { get; set; }
        public string DisplayContent()
        {
            return $@"
        Id: {Id}
        UserId: {UserId}
        Content: {Content}
        Timestamp: {Timestamp}
";
        }
    }
}
