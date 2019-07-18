using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.Storage;

namespace EsSharp
{
	public class EventStore
	{
		private readonly IEventSerializer _serializer;
		private readonly IEventDataStorage _dataStorage;
		private readonly IList<Aggregate> _aggregates;

		public EventStore(IEventSerializer serializer, IEventDataStorage dataStorage)
		{
			this._serializer = serializer;
			this._dataStorage = dataStorage;
			this._aggregates = new List<Aggregate>();
		}

		public IEnumerable<IEvent> GetEventsForAggregate(Guid aggregateId, int fromVersion = 0)
		{
			return this._dataStorage.Get(aggregateId, fromVersion).Select(x=>this._serializer.Deserialize<IEvent>(x.Data));
		}

		public void Add(Aggregate aggregate)
		{
			this._dataStorage.Add(aggregate.Events.Select(ev=>new SerializedEvent
			{
				AggregateId = ev.AggregateId,
				ExpectedVersion = ev.ExpectedVersion,
				EventType = ev.EventType,
				EventId = ev.EventId,
				Data = this._serializer.Serialize(ev)
			}));

			this._aggregates.Add(aggregate);
		}

		public void Commit()
		{
			this._dataStorage.Commit();
			foreach (var aggregate in this._aggregates)
			{
				aggregate.Commit();
			}
		}
	}
}
