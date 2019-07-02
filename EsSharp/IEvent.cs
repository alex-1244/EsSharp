using System;

namespace EsSharp
{
	public interface IEvent
	{
		Guid EventId { get; }
		Guid AggregateId { get; }
		int ExpectedVersion { get; }
		string EventType { get; }
	}
}
