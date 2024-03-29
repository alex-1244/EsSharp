﻿using System;

namespace EsSharp.ShopBoundedContext.Customers.Events
{
	[Serializable]
	public class CustomerCreated : IEvent
	{
		public CustomerCreated(Guid aggregateId, string name, Guid userId)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = 0;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.CustomerCreated.ToString();

			this.Name = name;
			this.UserId = userId;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public string Name { get; }
		public Guid UserId { get; }
	}
}
