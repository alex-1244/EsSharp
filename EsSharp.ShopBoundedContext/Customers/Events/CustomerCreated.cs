using System;
using EsSharp.ShopBoundedContext.Orders;

namespace EsSharp.ShopBoundedContext.Customers.Events
{
	public class CustomerCreated : IEvent
	{
		public CustomerCreated(Guid aggregateId, string name)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = 0;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.CustomerCreated.ToString();

			this.Name = name;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public string Name { get; }
	}

	public class CustomerCreatedOrder : IEvent
	{
		public CustomerCreatedOrder(Guid aggregateId, Order order)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = 0;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.CustomerCreated.ToString();

			this.Order = order;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public Order Order { get; }
	}

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

	public class BalanceFunded : IEvent
	{
		public BalanceFunded(Guid aggregateId, int version, int summ)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = version;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.CustomerCreated.ToString();

			this.Summ = summ;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public int Summ { get; }
	}
}
