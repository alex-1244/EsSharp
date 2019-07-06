using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EsSharp.Storage;

namespace EsSharp.Tests.TestEntities
{
	public class SnapshotDataStorage: ISnapshotDataStorage
	{
		public readonly List<AggregateSnapshot> _snapshotData;

		public SnapshotDataStorage()
		{
			this._snapshotData = new List<AggregateSnapshot>();
		}

		public AggregateSnapshot Get(Guid aggregateId)
		{
			var aggregateSnapshots = this._snapshotData.Where(x => x.AggregateId == aggregateId).OrderByDescending(x => x.AggregateVersion);

			return aggregateSnapshots.FirstOrDefault();
		}

		public void Add(AggregateSnapshot snapshot)
		{
			this._snapshotData.Add(snapshot);
		}

		public void Commit()
		{
		}
	}

	public class EventDataStorage : IEventDataStorage
	{
		public readonly List<IEvent> _events;

		public EventDataStorage()
		{
			this._events = new List<IEvent>();
		}

		public IEnumerable<IEvent> Get(Guid aggregateId, int version = 0)
		{
			return _events.Where(x => x.AggregateId == aggregateId && x.ExpectedVersion >= version);
		}

		public void Add(IEvent @event)
		{
			this._events.Add(@event);
		}

		public void Add(IEnumerable<IEvent> events)
		{
			this._events.AddRange(events);
		}

		public void Commit()
		{
		}
	}
}
