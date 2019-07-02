using System;
using System.Collections.Generic;
using System.Linq;

namespace EsSharp
{
	public class AggregateStore
	{
		private readonly EventStore _eventStore;
		private readonly IAggregateSerializer _serializer;
		private readonly List<AggregateSnapshot> _snapshotData;

		public AggregateStore(EventStore eventStore, IAggregateSerializer serializer)
		{
			_eventStore = eventStore;
			_serializer = serializer;
			this._snapshotData = new List<AggregateSnapshot>();
		}

		public T GetAggregate<T>(Guid aggregateId) where T : Aggregate, new()
		{
			var aggregateSnapshots = _snapshotData.Where(x => x.AggregateId == aggregateId).ToList();
			AggregateSnapshot aggregateData = null;
			Aggregate agg;

			if (aggregateSnapshots.Any())
			{
				aggregateData = aggregateSnapshots.OrderByDescending(x => x.AggregateVersion).First();
				agg = this._serializer.Deserialize<T>(aggregateData.AggregateData);
			}
			else
			{
				agg = new T();
			}

			var eventsForAggregate = this._eventStore.GetEventsForAggregate(aggregateId, aggregateData?.AggregateVersion ?? 0);

			foreach (var @event in eventsForAggregate)
			{
				agg.Handle(@event);
			}

			return (T)agg;
		}

		public void SaveAggregate(Aggregate aggregate)
		{
			foreach (var @event in aggregate.Events)
			{
				this._eventStore.Add(@event);
			}

			if (aggregate.Version - this.GetLastSnapshotVersion(aggregate.Id) > 5)
			{
				var aggSnapshot = new AggregateSnapshot(this._serializer.Serialize(aggregate), aggregate.Id, aggregate.Version);
				this._snapshotData.Add(aggSnapshot);
			}
		}

		private int GetLastSnapshotVersion(Guid aggregateId)
		{
			var versionHistory = _snapshotData.Where(x => x.AggregateId == aggregateId).Select(x => x.AggregateVersion).ToList();
			if (versionHistory.Any())
			{
				return versionHistory.Max();
			}

			return 0;
		}
	}
}