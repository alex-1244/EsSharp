using System;

namespace EsSharp.ShopBoundedContext.Orders.Events
{
	public class OrderPayed : IEvent
	{
		public OrderPayed(Guid aggregateId, int version)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = version;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.OrderCreated.ToString();
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }
	}
}