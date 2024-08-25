using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Behaviours
{
    internal class Subcriber : ISubcriber<Post>, Interfaces.IObserver<Post>
    {
        private readonly IMediator<Post, User> _mediator;
        public Guid SubcriberId { get; }
        public Guid UserId { get; }
        public Guid PublisherId { get; }

        private readonly Stack<Post> _postStack = new();

        public Subcriber(IMediator<Post, User> mediator, Guid subcriberId, Guid userId, Guid publisherId)
        {
            this._mediator = mediator;
            SubcriberId = subcriberId;
            UserId = userId;
            PublisherId = publisherId;
        }


        public IEnumerable<Post> ReceiveAllMessage()
        {
            var result = this._postStack.ToList();
            _postStack.Clear();
            return result;
        }

        public Post ReceiveLatestMessage()
            => _postStack.Pop();

        public void Unsubcribe()
        {
            _mediator.Unsubcribe(SubcriberId);
        }

        public void OnNotify(Post message)
        {
            this._postStack.Push(message);
        }

        public bool HasNew()
            => _postStack.Count > 0;
    }
}
