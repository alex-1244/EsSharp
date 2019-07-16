using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.Storage;

namespace EsSharp.Tests.TestEntities
{
	public class SnapshotDataStorage: ISnapshotDataStorage
	{
		public List<AggregateSnapshot> SnapshotData { get; }

		public SnapshotDataStorage()
		{
			this.SnapshotData = new List<AggregateSnapshot>();
		}

		public AggregateSnapshot Get(Guid aggregateId)
		{
			var aggregateSnapshots = this.SnapshotData.Where(x => x.AggregateId == aggregateId).OrderByDescending(x => x.AggregateVersion);

			return aggregateSnapshots.FirstOrDefault();
		}

		public void Add(AggregateSnapshot snapshot)
		{
			this.SnapshotData.Add(snapshot);
		}

		public void Commit()
		{
		}
	}

	public class EventDataStorage : IEventDataStorage
	{
		public HashSet<SerializedEvent> Events { get; }

		public EventDataStorage()
		{
			this.Events = new HashSet<SerializedEvent>();
		}

		public IEnumerable<SerializedEvent> Get(Guid aggregateId, int version = 0)
		{
			return this.Events.Where(x => x.AggregateId == aggregateId && x.ExpectedVersion >= version).OrderBy(x=>x.ExpectedVersion);
		}

		public void Add(SerializedEvent @event)
		{
			this.Events.Add(@event);
		}

		public void Add(IEnumerable<SerializedEvent> events)
		{
			this.Events.UnionWith(events);
		}

		public void Commit()
		{
		}
	}
}
