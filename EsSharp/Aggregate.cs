using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace EsSharp
{
	[Serializable]
	public abstract class Aggregate : IEquatable<Aggregate>
	{
		[NonSerialized]
		private IList<IEvent> _events;

		protected Aggregate()
		{
			this._events = new List<IEvent>();
		}

		public virtual IEnumerable<IEvent> Events => _events.AsEnumerable();

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

			if (this.Id != @event.AggregateId)
			{
				throw new VersionNotFoundException($"event {@event.EventId} does not belong to this aggreagate");
			}

			this.HandleInternal(@event);

			this.Version++;
		}

		protected abstract void HandleInternal(IEvent @event);

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			this._events = new List<IEvent>();
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as Aggregate);
		}

		public bool Equals(Aggregate other)
		{
			return other != null &&
				   this.Id.Equals(other.Id);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Id);
		}

		public static bool operator ==(Aggregate aggregate1, Aggregate aggregate2)
		{
			return EqualityComparer<Aggregate>.Default.Equals(aggregate1, aggregate2);
		}

		public static bool operator !=(Aggregate aggregate1, Aggregate aggregate2)
		{
			return !(aggregate1 == aggregate2);
		}
	}
}
