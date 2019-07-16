using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EsSharp.Tests.TestEntities.Events
{
	[Serializable]
	public class TyreChangedEvent : IEvent
	{
		public TyreChangedEvent(Guid aggId, int expectedVersion, Tyre newTyre, TyrePlacement placement)
		{
			this.AggregateId = aggId;
			this.EventId = Guid.NewGuid();
			this.EventType = EventTypes.TyreChanged.ToString();
			this.ExpectedVersion = expectedVersion;
			this.Tyre = newTyre;
			this.Placement = placement;
		}

		public Guid EventId { get; set; }
		public Guid AggregateId { get; set; }
		public int ExpectedVersion { get; set; }
		public string EventType { get; set; }
		public Tyre Tyre { get; }
		public TyrePlacement Placement { get; }

		public byte[] Serialize()
		{
			var bf = new BinaryFormatter();
			using (var ms = new MemoryStream())
			{
				bf.Serialize(ms, this);
				return ms.ToArray();
			}
		}
	}
}