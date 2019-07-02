using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EsSharp;

namespace EsSharp.Tests.TestEntities
{
	public class DefaultEventSerializer : IEventSerializer
	{
		private readonly BinaryFormatter _formatter;

		public DefaultEventSerializer()
		{
			this._formatter = new BinaryFormatter();
		}

		public byte[] Serialize(IEvent ev)
		{
			using (var ms = new MemoryStream())
			{
				this._formatter.Serialize(ms, ev);
				return ms.ToArray();
			}
		}

		public T Deserialize<T>(byte[] evData) where T : IEvent
		{
			using (var memStream = new MemoryStream())
			{
				memStream.Write(evData, 0, evData.Length);
				memStream.Seek(0, SeekOrigin.Begin);
				var obj = (T)this._formatter.Deserialize(memStream);
				return obj;
			}
		}
	}

	public class DefaultAggregateSerializer : IAggregateSerializer
	{
		private readonly BinaryFormatter _formatter;

		public DefaultAggregateSerializer()
		{
			this._formatter = new BinaryFormatter();
		}

		public byte[] Serialize(Aggregate agg)
		{
			using (var ms = new MemoryStream())
			{
				this._formatter.Serialize(ms, agg);
				return ms.ToArray();
			}
		}

		public T Deserialize<T>(byte[] aggData) where T : Aggregate
		{
			using (var memStream = new MemoryStream())
			{
				memStream.Write(aggData, 0, aggData.Length);
				memStream.Seek(0, SeekOrigin.Begin);
				var obj = (T)this._formatter.Deserialize(memStream);
				return obj;
			}
		}
	}
}