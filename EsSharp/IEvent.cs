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

	public sealed class SerializedEvent
	{
		public Guid EventId { get; set; }
		public Guid AggregateId { get; set; }
		public int ExpectedVersion { get; set; }
		public string EventType { get; set; }
		public byte[] Data { get; set; }
	}
}
