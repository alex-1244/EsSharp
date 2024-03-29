﻿using System;
using EsSharp.ShopBoundedContext.Orders;

namespace EsSharp.ShopBoundedContext.Customers.Events
{
	[Serializable]
	public class CustomerCreatedOrder : IEvent
	{
		public CustomerCreatedOrder(Guid aggregateId, int version, Order order)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = version;
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
}