using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace EsSharp
{
	[Serializable]
	public abstract class Aggregate
	{
		private readonly IList<IEvent> _events;

		protected Aggregate()
		{
			this._events = new List<IEvent>();
		}

		public IEnumerable<IEvent> Events => _events.AsEnumerable();

		public Guid Id { get; protected set; }

		public int Version { get; protected set; }

		protected void PublishEvent(IEvent domainEvent)
		{
			this._events.Add(domainEvent);
			this.Version++;
		}

		public void Handle(IEvent @event)
		{
			if (this.Version != @event.ExpectedVersion)
			{
				throw new VersionNotFoundException($"event {@event.EventId} expected version was {@event.ExpectedVersion}, but aggregate {@event.AggregateId} version was {this.Version}");
			}

			this.HandleInternal(@event);

			this.Version++;
		}

		protected abstract void HandleInternal(IEvent @event);

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			this._events.Clear();
		}
	}
}
