﻿using System;

namespace EsSharp.ShopBoundedContext.Orders.Events
{
	[Serializable]
	public class ProductAdded : IEvent
	{
		public ProductAdded(Guid aggregateId, int version, Product product)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = version;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.ProductAdded.ToString();

			this.Product = product;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public Product Product { get; }
	}
}