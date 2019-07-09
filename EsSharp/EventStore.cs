using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EsSharp.Storage;

namespace EsSharp
{
	public class EventStore
	{
		private readonly IEventSerializer _serializer;
		private readonly IEventDataStorage _dataStorage;

		public EventStore(IEventSerializer serializer, IEventDataStorage dataStorage)
		{
			this._serializer = serializer;
			this._dataStorage = dataStorage;
		}

		public IEnumerable<IEvent> GetEventsForAggregate(Guid aggregateId, int fromVersion = 0)
		{
			return this._dataStorage.Get(aggregateId, fromVersion).Select(x=>this._serializer.Deserialize<IEvent>(x.data));
		}

		public void Add(Aggregate aggregate)
		{
			this._dataStorage.Add(aggregate.Events.Select(ev=>new SerializedEvent
			{
				AggregateId = ev.AggregateId,
				ExpectedVersion = ev.ExpectedVersion,
				EventType = ev.EventType,
				EventId = ev.EventId,
				data = this._serializer.Serialize(ev)
			}));
		}

		public void Commit()
		{
			this._dataStorage.Commit();
		}
	}
}
