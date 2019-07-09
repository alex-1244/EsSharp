﻿using System;

namespace EsSharp.ShopBoundedContext.Orders.Events
{
	public class OrderClosed : IEvent
	{
		public OrderClosed(Guid aggregateId, int expectedVersion)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = expectedVersion;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.OrderCancelled.ToString();
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }
	}
}