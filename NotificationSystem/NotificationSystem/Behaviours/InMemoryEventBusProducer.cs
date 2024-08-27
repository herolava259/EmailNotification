using NotificationSystem.Interfaces;
using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace NotificationSystem.Behaviours
{
    internal sealed class InMemoryEventBusProducer<T> : IProducer<T>
    {
        private readonly ChannelWriter<Event<T>> _bus;

        public InMemoryEventBusProducer(ChannelWriter<Event<T>> bus)
        {
            _bus = bus;
        }

        public ValueTask DisposeAsync()
        {
            _bus.TryComplete();
            return ValueTask.CompletedTask;
        }

        public async ValueTask Publish(Event<T> @event, CancellationToken token = default)
        {
            await _bus.WriteAsync(@event, token).ConfigureAwait(false);
        }
    }
}
