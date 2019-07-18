using System;
using EsSharp.ShopBoundedContext.Customers;

namespace EsSharp.ShopBoundedContext.Orders.Events
{
	[Serializable]
	public class OrderCreated : IEvent
	{
		public OrderCreated(Guid aggregateId, Customer customer)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = 0;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.OrderCreated.ToString();

			this.Customer = customer;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public Customer Customer { get; }
	}
}
