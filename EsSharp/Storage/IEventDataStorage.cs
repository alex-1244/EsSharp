using System;
using System.Collections.Generic;

namespace EsSharp.Storage
{
	public interface IEventDataStorage
	{
		IEnumerable<SerializedEvent> Get(Guid aggregateId, int version = 0);
		void Add(SerializedEvent @event);
		void Add(IEnumerable<SerializedEvent> events);
		void Commit();
	}
}
