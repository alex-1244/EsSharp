using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EsSharp
{
	public class EventStore
	{
		private readonly IEventSerializer _serializer;
		private readonly IList<IEvent> _events;

		protected IEnumerable<IEvent> Events => this._events.AsEnumerable();

		public EventStore(IEventSerializer serializer)
		{
			this._serializer = serializer;
			_events = new List<IEvent>();
		}

		public IEnumerable<IEvent> GetEventsForAggregate(Guid aggregateId, int fromVersion = 0)
		{
			return this._events.Where(x =>
				x.AggregateId == aggregateId
				&& x.ExpectedVersion >= fromVersion).OrderBy(x=>x.ExpectedVersion);
		}

		public void Add(IEvent ev)
		{
			this._events.Add(ev);
		}

		public void Commit()
		{
			//save events to DB here
			this._events.Clear();
		}
	}
}
