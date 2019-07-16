using System;
using EsSharp.ShopBoundedContext.Orders;

namespace EsSharp.ShopBoundedContext.Customers.Events
{
	[Serializable]
	public class OrderWasPayed : IEvent
	{
		public OrderWasPayed(Guid aggregateId, int version, Order order, int summ)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = version;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.CustomerCreated.ToString();

			this.Order = order;
			this.Summ = summ;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public Order Order { get; }
		public int Summ { get; }
	}
}