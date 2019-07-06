using System;
using EsSharp.Storage;

namespace EsSharp
{
	public class AggregateStore
	{
		private readonly EventStore _eventStore;
		private readonly IAggregateSerializer _serializer;
		private readonly ISnapshotDataStorage _snapshotDataStorage;

		public AggregateStore(
			EventStore eventStore,
			IAggregateSerializer serializer,
			ISnapshotDataStorage snapshotDataStorage)
		{
			this._eventStore = eventStore;
			this._serializer = serializer;
			this._snapshotDataStorage = snapshotDataStorage;
		}

		public T Get<T>(Guid aggregateId) where T : Aggregate, new()
		{
			var aggregateData = this._snapshotDataStorage.Get(aggregateId);
			Aggregate agg;

			if (aggregateData != null)
			{
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

		public void Save(Aggregate aggregate)
		{
			foreach (var @event in aggregate.Events)
			{
				this._eventStore.Add(@event);
			}

			var aggSnapshot = new AggregateSnapshot(this._serializer.Serialize(aggregate), aggregate.Id, aggregate.Version);
			this._snapshotDataStorage.Add(aggSnapshot);
			this._snapshotDataStorage.Commit();
		}
	}
}