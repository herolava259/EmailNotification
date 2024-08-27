using NotificationSystem.Interfaces;
using NotificationSystem.Models;

namespace NotificationSystem.Behaviours
{
    internal class NotificationSystem : IMediator<Post, User>
    {
        private readonly IDictionary<Guid, List<Guid>> notificationDict ;
        private readonly IDictionary<Guid, Subcriber> subcriberDict;

        private readonly List<Publisher> publisherList;
        public NotificationSystem()
        {
            notificationDict = new Dictionary<Guid, List<Guid>>();
            subcriberDict = new Dictionary<Guid, Subcriber>();
            publisherList = new();
        }
        public void PushMessage(Post message, Guid publisherId)
        {
            foreach(var subcriberId in notificationDict[publisherId])
            {
                if(subcriberDict.TryGetValue(subcriberId, out var subcriber))
                {
                    subcriber.OnNotify(message);
                }
            }
        }


        public IPublisher RegisterToPublisher(User user)
        {
            var publisher = publisherList.Find(c => c.UserId == user.GetUserId());

            if (publisher is not null)
                return publisher;

            var publisherId = Guid.NewGuid();
            Publisher newPublisher = new Publisher(this, publisherId, user.GetUserId());

            notificationDict.Add(publisherId, new());
            publisherList.Add(newPublisher);
            return newPublisher;
            
        }

        public ISubcriber<Post>? Subcribe(User user, Guid otherId)
        {
            var publisher = publisherList.Find(c => c.UserId == otherId);

            if (publisher == null)
                return default;
            var newId = Guid.NewGuid();
            var newSubcriber = new Subcriber(this, newId, user.GetUserId(), publisher.PublisherId);

            subcriberDict.Add(newId, newSubcriber);
            notificationDict[publisher.PublisherId].Add(newId);
            user.PublisherIds.Add(newId);
            return newSubcriber;
        }

        public void Unsubcribe(Guid subcriberId)
        {
            if (!subcriberDict.TryGetValue(subcriberId, out var subcriber))
                return;
            notificationDict[subcriber!.PublisherId].Remove(subcriberId);
            subcriberDict.Remove(subcriberId);
        }

        
        
    }
}
