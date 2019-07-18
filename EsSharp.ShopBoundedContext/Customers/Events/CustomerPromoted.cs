using System;

namespace EsSharp.ShopBoundedContext.Customers.Events
{
	[Serializable]
	public class CustomerPromoted : IEvent
	{
		public CustomerPromoted(Guid aggregateId, int version)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = version;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.CustomerPromoted.ToString();
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }
	}
}