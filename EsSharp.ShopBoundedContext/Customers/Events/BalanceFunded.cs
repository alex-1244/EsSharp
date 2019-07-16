using System;

namespace EsSharp.ShopBoundedContext.Customers.Events
{
	[Serializable]
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