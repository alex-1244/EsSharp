using System;
using EsSharp.ShopBoundedContext.Orders;

namespace EsSharp.ShopBoundedContext.Managers.Events
{
	public class ManagerCreated: IEvent
	{
		public ManagerCreated(Guid aggregateId, string name)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = 0;
			this.EventId = Guid.NewGuid();
			this.EventType = ManagerEventTypes.ManagerCreated.ToString();

			this.Name = name;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public string Name { get; }
	}

	public class ManagerAssigned : IEvent
	{
		public ManagerAssigned(Guid aggregateId, Order order)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = 0;
			this.EventId = Guid.NewGuid();
			this.EventType = ManagerEventTypes.ManagerAssigned.ToString();

			this.Order = order;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public Order Order { get; }
	}

	public class ManagerUnassigned : IEvent
	{
		public ManagerUnassigned(Guid aggregateId, Order order)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = 0;
			this.EventId = Guid.NewGuid();
			this.EventType = ManagerEventTypes.ManagerUnassigned.ToString();

			this.Order = order;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public Order Order { get; }
	}

	public class ManagerClosedOrder : IEvent
	{
		public ManagerClosedOrder(Guid aggregateId, Order order)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = 0;
			this.EventId = Guid.NewGuid();
			this.EventType = ManagerEventTypes.ManagerClosedOrder.ToString();
			this.Order = order;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public Order Order { get; }
	}
}
