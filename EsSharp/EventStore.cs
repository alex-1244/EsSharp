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
			return this._dataStorage.Get(aggregateId, fromVersion);
		}

		public void Add(IEvent ev)
		{
			this._dataStorage.Add(ev);
		}

		public void Commit()
		{
			//save events to DB here
			this._dataStorage.Commit();
		}
	}
}
