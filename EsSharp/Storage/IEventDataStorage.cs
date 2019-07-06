using System;
using System.Collections.Generic;
using System.Text;

namespace EsSharp.Storage
{
	public interface IEventDataStorage
	{
		IEnumerable<IEvent> Get(Guid aggregateId, int version = 0);
		void Add(IEvent @event);
		void Add(IEnumerable<IEvent> events);
		void Commit();
	}
}
