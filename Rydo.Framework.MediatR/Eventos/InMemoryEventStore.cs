using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rydo.Framework.MediatR.Eventos
{
    public class InMemoryEventStore : IEventStore
    {
        //Use this to publish events so that event handlers can consume them
        private readonly IEventPublisher _publisher;
        private readonly IDictionary<Guid, List<IEvent>> _inMemoryDb;

        public InMemoryEventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
            _inMemoryDb = new Dictionary<Guid, List<IEvent>>();
        }

        public Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion)
        {
            List<IEvent> events;
            _inMemoryDb.TryGetValue(aggregateId, out events);

            var returnEvents = events?.Where(x => x.Version > fromVersion) ?? new List<IEvent>();

            return Task.FromResult(returnEvents);
        }

        public async Task Save(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                List<IEvent> list;

                _inMemoryDb.TryGetValue(@event.Id, out list);

                if (list == null)
                {
                    list = new List<IEvent>();
                    _inMemoryDb.Add(@event.Id, list);
                }

                list.Add(@event);
                await _publisher.Publish(@event);
            }
        }
    }
}
