using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Behaviours
{
    internal class Publisher : IPublisher
    {
        private readonly IMediator<Post, User> _mediator;

        public Publisher(IMediator<Post, User> mediator, Guid publisherId, Guid userId)
        {
            this._mediator = mediator;
            PublisherId = publisherId;
            UserId = userId;
        }

        public Guid PublisherId { get; }
        public Guid UserId { get; }

        public void PublishBulkMessages(IEnumerable<string> contents)
        {
            foreach (var content in contents) { 
                this.PublishMessage(content);
            }
        }

        public void PublishMessage(string content)
        {
            var newMessage = new Post
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                Content = content,
                Timestamp = DateTime.Now,
            };
            this._mediator.PushMessage(newMessage, PublisherId);
        }

       
    }
}
