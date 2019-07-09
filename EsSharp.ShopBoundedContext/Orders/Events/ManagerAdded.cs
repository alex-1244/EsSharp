using System;
using EsSharp.ShopBoundedContext.Managers;

namespace EsSharp.ShopBoundedContext.Orders.Events
{
	public class ManagerAdded : IEvent
	{
		public ManagerAdded(Guid aggregateId, int expectedVersion, Manager manager)
		{
			this.AggregateId = aggregateId;
			this.ExpectedVersion = expectedVersion;
			this.EventId = Guid.NewGuid();
			this.EventType = OrderEventTypes.ManagerAdded.ToString();

			this.Manager = manager;
		}

		public Guid EventId { get; }
		public Guid AggregateId { get; }
		public int ExpectedVersion { get; }
		public string EventType { get; }

		public Manager Manager { get; }
	}
}