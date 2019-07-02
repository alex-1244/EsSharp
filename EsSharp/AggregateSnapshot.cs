using System;

namespace EsSharp
{
	public class AggregateSnapshot
	{
		public AggregateSnapshot(byte[] data, Guid id, int version)
		{
			this.AggregateData = data;
			this.AggregateId = id;
			this.AggregateVersion = version;
		}

		public byte[] AggregateData { get; }
		public Guid AggregateId { get; }
		public int AggregateVersion { get; }
	}
}